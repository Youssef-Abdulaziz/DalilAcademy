using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Mappings;
using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DalilAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(IStudentService studentService) : ControllerBase
    {
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]

        public async Task<ActionResult<PagedResult<StudentResponseDto>>> GetAll(
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return await studentService.GetPagedAsync(page, pageSize);
        }
        [Authorize(Roles = Roles.Admin)]

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentResponseDto>> GetById(int id)
        {
            return await studentService.GetByIdAsync(id);

        }
        [Authorize(Roles = Roles.Admin)]

        [HttpPost]
        public async Task<ActionResult<StudentResponseDto>> Create(CreateStudentDto dto)
        {
            var created = await studentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, UpdateStudentDto dto)
        {
            await studentService.UpdateAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<ActionResult> Delete(int id)
        {
            await studentService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("{id:int}/courses")]
        public async Task<ActionResult<List<CourseResponseDto>>> GetCourses(int id)
        {
            return await studentService.GetCoursesAsync(id);
        }
    }
}
