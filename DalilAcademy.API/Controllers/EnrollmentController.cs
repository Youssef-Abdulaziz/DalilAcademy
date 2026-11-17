using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Enums;
using DalilAcademy.Core.Payments;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using DalilAcademy.Infra.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DalilAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController(IEnrollmentService enrollmentService, IStudentRepository students, IPaymentGateway gateway, IPaymentService paymentService) : ControllerBase
    {
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<ActionResult<List<EnrollmentResponseDto>>> GetAll()
        {
            return await enrollmentService.GetAllAsync();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EnrollmentResponseDto>> GetById(int id)
        {
            return await enrollmentService.GetByIdAsync(id);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<ActionResult<EnrollmentResponseDto>> Create(CreateEnrollmentDto dto)
        {
            var created = await enrollmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, UpdateEnrollmentDto dto)
        {
            await enrollmentService.UpdateAsync(id, dto);
            return NoContent();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await enrollmentService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Roles = Roles.Student)]
        [HttpGet("me")]
        public async Task<IActionResult> MyEnrollments()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var student = await students.GetByUserIdAsync(userId);   // Include Enrollments → Course
            if (student is null) return NotFound("No student profile linked to this account.");

            return Ok(student.Enrollments
                .Where(e => e.Status == EnrollmentStatus.Paid)       // rule 4: Paid only
                .Select(e => new
                {
                    e.Id,
                    Course = e.Course.Title,
                    e.EnrolledAt
                }));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("test-paypal")]
        public async Task<IActionResult> TestPayPal()
        {
            var order = await gateway.CreateOrderAsync(10m);
            return Ok(order);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("{id:int}/pay")]
        public async Task<ActionResult<PayResponseDto>> Pay(int id)
        {
            return await paymentService.PayAsync(id);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("{id:int}/capture")]
        public async Task<ActionResult<EnrollmentResponseDto>> Capture(int id)
        {
            return await paymentService.CaptureAsync(id);
        }
    }
}