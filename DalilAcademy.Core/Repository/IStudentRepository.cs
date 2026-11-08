using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student> CreateAsync(Student student);
        Task<bool> UpdateAsync(int id, Student updated);
        Task<bool> DeleteAsync(int id);

        Task<bool> EmailExistsAsync(string email);

        Task<List<Course>?> GetCoursesAsync(int studentId);

        Task<(List<Student> Items, int TotalCount)> GetPagedAsync(int page, int pageSize);

        Task<Student?> GetByUserIdAsync(int userId);
    }
}
