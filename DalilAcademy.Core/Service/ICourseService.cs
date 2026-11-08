using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.DTOs;

namespace DalilAcademy.Core.Service
{
    public interface ICourseService
    {
        Task<PagedResult<CourseResponseDto>> GetAllAsync(int page, int pageSize, decimal? minPrice, decimal? maxPrice);
        Task<CourseResponseDto?> GetByIdAsync(int id);
        Task<CourseResponseDto> CreateAsync(CreateCourseDto dto);
        Task UpdateAsync(int id, Course updated);
        Task DeleteAsync(int id);
    }
}
