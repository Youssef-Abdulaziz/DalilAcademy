using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Service
{
    public interface IStudentService
    {
        Task<PagedResult<StudentResponseDto>> GetPagedAsync(int page, int pageSize);
        Task<StudentResponseDto?> GetByIdAsync(int id);
        Task<StudentResponseDto> CreateAsync(CreateStudentDto dto);
        Task UpdateAsync(int id, UpdateStudentDto dto);
        Task DeleteAsync(int id);

        Task<List<CourseResponseDto>> GetCoursesAsync(int id);

        
        
    }
}
