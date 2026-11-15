using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Enums;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Repository
{
    public class PaymentRepository(AppDbContext db) : IPaymentRepository
    {
        public async Task<Payment> CreateAsync(Payment payment)
        {
            db.Payments.Add(payment);
            await db.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment?> GetLatestCreatedAsync(int enrollmentId)
        {
            return await db.Payments
                .Where(p => p.EnrollmentId == enrollmentId && p.Status == PaymentStatus.Created)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefaultAsync();      // tracked, so changes can be saved
        }

        public async Task UpdateAsync(Payment payment)
        {
            await db.SaveChangesAsync();
        }
    }
}
