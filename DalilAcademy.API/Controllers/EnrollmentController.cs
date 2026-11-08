using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DalilAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController(IEnrollmentService enrollmentService, IStudentRepository students) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = Roles.Student)]

        public async Task<ActionResult<EnrollmentResponseDto>> Create(CreateEnrollmentDto dto)
        {
            var created = await enrollmentService.CreateAsync(dto);
            return Created($"/api/enrollments/{created.Id}", created);
        }

        [Authorize(Roles = Roles.Student)]
        [HttpGet("me")]
        public async Task<IActionResult> MyEnrollments()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var student = await students.GetByUserIdAsync(userId);   // Include Enrollments → Course
            if (student is null) return NotFound("No student profile linked to this account.");

            return Ok(student.Enrollments.Select(e => new
            {
                e.Id,
                Course = e.Course.Title,      // use your Course's actual property name
                e.EnrolledAt
            }));
        }
    }
}
