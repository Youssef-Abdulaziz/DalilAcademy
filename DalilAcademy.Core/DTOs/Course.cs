using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.DTOs
{
    
        public record CreateCourseDto ( string Title,decimal Price,int InstructorId, string? ImageUrl);
        public record UpdateCourseDto(string Title, decimal Price, string? ImageUrl);
        public record CourseResponseDto(int Id, string Title, decimal Price, string? InstructorName, string? ImageUrl);

    
}
