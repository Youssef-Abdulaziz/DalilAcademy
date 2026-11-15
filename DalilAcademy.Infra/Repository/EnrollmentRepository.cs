using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Enums;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace DalilAcademy.Infra.Repository
{
    public class EnrollmentRepository(AppDbContext db) : IEnrollmentRepository
    {
        public async Task<List<Enrollment>> GetAllAsync()
        {
            return await db.Enrollments.AsNoTracking().OrderBy(e => e.Id).ToListAsync();
        }

        public async Task<Enrollment?> GetByIdAsync(int id)
        {
            return await db.Enrollments
                .Include(e => e.Course)                  // tracked, so it can be updated and saved
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> ExistsAsync(int studentId, int courseId)
        {
            return await db.Enrollments.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }

        public async Task<List<CourseResponseDto>> GetPaidCoursesByStudentAsync(int studentId)
        {
            return await db.Enrollments
                .Where(e => e.StudentId == studentId && e.Status == EnrollmentStatus.Paid)
                .Select(e => new CourseResponseDto(
                    e.Course!.Id, e.Course.Title, e.Course.Price, e.Course.Instructor!.FullName, e.Course.ImageUrl))
                .ToListAsync();
        }

        public async Task<Enrollment> CreateAsync(Enrollment enrollment)
        {
            db.Enrollments.Add(enrollment);
            await db.SaveChangesAsync();
            return enrollment;
        }

        public async Task UpdateAsync(Enrollment enrollment)
        {
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Enrollment enrollment)
        {
            db.Enrollments.Remove(enrollment);
            await db.SaveChangesAsync();
        }
    }
}