using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Service;
using DalilAcademy.Core.DTOs;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
namespace DalilAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController(ICourseService courseService) : ControllerBase
    {
        //private readonly ICourseService _courseService;
        //public CoursesController(ICourseService courseService) 
        //{
        //    _courseService = courseService;
        //}

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PagedResult<CourseResponseDto>>> GetAllAsync(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null)
        {
            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 1, 50);
            return Ok(await courseService.GetAllAsync(page, pageSize, minPrice, maxPrice));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CourseResponseDto>> GetById(int id) 
        {
            var course = await courseService.GetByIdAsync(id);
            if(course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<CourseResponseDto>> Create(CreateCourseDto dto)
        {
            
                var created = await courseService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<ActionResult> Update(int id, Course updated)
        {

            await courseService.UpdateAsync(id, updated);
            return NoContent();
    
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<ActionResult> Delete(int id)
        {
            await courseService.DeleteAsync(id); return NoContent();
        }


    }
}
