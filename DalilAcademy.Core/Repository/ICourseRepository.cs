using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Repository
{
    public interface ICourseRepository
    {
        Task<PagedResult<CourseResponseDto>> GetAllAsync(int page, int pageSize, decimal? minPrice, decimal? maxPrice);        
        Task<Course?> GetByIdAsync(int id);
        Task<Course> CreateAsync(Course course);
        Task UpdateAsync( Course updated);
        Task<bool> DeleteAsync(int id);
    }
}
