using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using DalilAcademy.Core.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalilAcademy.Core.Mappings;
using FluentValidation;
using DalilAcademy.Core.Exceptions;
namespace DalilAcademy.Infra.Service
{
    public class CourseService(ICourseRepository courseRepository, IValidator<CreateCourseDto> validator) : ICourseService
    {
        public Task<PagedResult<CourseResponseDto>> GetAllAsync(int page, int pageSize, decimal? minPrice, decimal? maxPrice)
        {
            return courseRepository.GetAllAsync(page, pageSize, minPrice, maxPrice);
            
        }
        public async Task<CourseResponseDto> GetByIdAsync(int id) 
        {
            var course = await courseRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Course {id} was not found") ;
            return course?.ToDto();
        }
        public async Task<CourseResponseDto> CreateAsync(CreateCourseDto dto)
        {
            await validator.ValidateAndThrowAsync(dto);
            var course = dto.ToEntity(); 
            var created = await courseRepository.CreateAsync(course);
            return created.ToDto();
        }

        public async Task UpdateAsync(int id, Course updated)
        {
            var course = await courseRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Course {id} was not found");
            
 
            course.Title= updated.Title;
            course.Price= updated.Price;

            await courseRepository.UpdateAsync(updated);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await courseRepository.DeleteAsync(id))
                 throw new NotFoundException($"Course {id} was not found");
        }
    }
}
