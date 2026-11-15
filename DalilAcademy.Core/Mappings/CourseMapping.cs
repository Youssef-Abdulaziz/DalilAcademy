using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;

namespace DalilAcademy.Core.Mappings
{
    public static class CourseMapping
    {
        public static CourseResponseDto ToDto(this Course course) 
        {
            return new CourseResponseDto(course.Id, course.Title, course.Price, course.Instructor?.FullName, course.ImageUrl);
        }

        public static Course ToEntity(this CreateCourseDto dto)
        {
            return new Course
            {
                Title = dto.Title, 
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,    
                InstructorId = dto.InstructorId 
            };
        }
    }
}
