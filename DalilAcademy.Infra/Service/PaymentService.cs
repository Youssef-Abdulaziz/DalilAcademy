using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Enums;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Mappings;
using DalilAcademy.Core.Payments;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using DalilAcademy.Infra.Payments;
using Microsoft.Extensions.Options;

namespace DalilAcademy.Infra.Service
{
    public class PaymentService(
        IEnrollmentRepository enrollmentRepository,
        IPaymentRepository paymentRepository,
        IPaymentGateway gateway,
        IOptions<PayPalSettings> options) : IPaymentService
    {
        public async Task<PayResponseDto> PayAsync(int enrollmentId)
        {
            var enrollment = await enrollmentRepository.GetByIdAsync(enrollmentId)
                ?? throw new NotFoundException($"Enrollment {enrollmentId} was not found.");      // 404

            if (enrollment.Status == EnrollmentStatus.Paid)
                throw new ConflictException("This enrollment is already paid.");                  // 409

            // The amount is calculated on the server, never taken from the request
            var amount = Math.Round(enrollment.Course!.Price * options.Value.JodToUsdRate, 2);

            var order = await gateway.CreateOrderAsync(amount);

            var payment = await paymentRepository.CreateAsync(new Payment
            {
                EnrollmentId = enrollment.Id,
                Amount = amount,
                Currency = "USD",
                PayPalOrderId = order.OrderId,
                Status = PaymentStatus.Created,
                CreatedAt = DateTime.UtcNow
            });

            return new PayResponseDto(payment.Id, order.OrderId, amount, "USD", order.ApprovalUrl);
        }

        public async Task<EnrollmentResponseDto> CaptureAsync(int enrollmentId)
        {
            var enrollment = await enrollmentRepository.GetByIdAsync(enrollmentId)
                ?? throw new NotFoundException($"Enrollment {enrollmentId} was not found.");      // 404

            if (enrollment.Status == EnrollmentStatus.Paid)
                throw new ConflictException("This enrollment is already paid.");                  // 409

            var payment = await paymentRepository.GetLatestCreatedAsync(enrollmentId)
                ?? throw new PaymentFailedException("Call /pay first.");                          // 400

            var completed = await gateway.CaptureOrderAsync(payment.PayPalOrderId);

            if (!completed)
            {
                payment.Status = PaymentStatus.Failed;                  // enrollment stays Pending
                await paymentRepository.UpdateAsync(payment);
                throw new PaymentFailedException(
                    "The payment was not completed. The order may not be approved yet, or it expired. Call /pay again.");   // 400
            }

            payment.Status = PaymentStatus.Completed;
            payment.PaidAt = DateTime.UtcNow;
            enrollment.Status = EnrollmentStatus.Paid;

            await paymentRepository.UpdateAsync(payment);   // one SaveChanges saves both tracked entities

            return enrollment.ToDto();
        }
    }
}