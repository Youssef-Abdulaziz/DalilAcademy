using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Repository
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllAsync();
        Task<Enrollment?> GetByIdAsync(int id);          // includes Course (PaymentService needs the price)
        Task<bool> ExistsAsync(int studentId, int courseId);
        Task<List<CourseResponseDto>> GetPaidCoursesByStudentAsync(int studentId);
        Task<Enrollment> CreateAsync(Enrollment enrollment);
        Task UpdateAsync(Enrollment enrollment);         // saves changes to a tracked enrollment
        Task DeleteAsync(Enrollment enrollment);
    }
}
