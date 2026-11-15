using DalilAcademy.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Service
{
    public interface IEnrollmentService
    {
        Task<List<EnrollmentResponseDto>> GetAllAsync();
        Task<EnrollmentResponseDto> GetByIdAsync(int id);
        Task<EnrollmentResponseDto> CreateAsync(CreateEnrollmentDto dto);
        Task UpdateAsync(int id, UpdateEnrollmentDto dto);
        Task DeleteAsync(int id);
        Task<List<CourseResponseDto>> GetMyCoursesAsync(int studentId);
    }
}
