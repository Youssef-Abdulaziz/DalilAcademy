using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Repository
{
    public class StudentRepository(AppDbContext db) : IStudentRepository
    {
        public async Task<List<Student>> GetAllAsync()
        {
            return await db.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await db.Students.FindAsync(id);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            db.Students.Add(student);
            await db.SaveChangesAsync();
            return student;
        }

        public async Task<bool> UpdateAsync(int id, Student updated)
        {
            var student = await db.Students.FindAsync(id);
            if (student is null)
                return false;

            student.FullName = updated.FullName;
            student.Email = updated.Email;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await db.Students.FindAsync(id);
            if (student is null)
                return false;

            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await db.Students.AnyAsync(s => s.Email.ToLower() == email.ToLower());
        }

        public async Task<List<Course>?> GetCoursesAsync(int studentId)
        {
            var student = await db.Students
                .AsNoTracking()
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                        .ThenInclude(c => c!.Instructor)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            return student?.Enrollments.Select(e => e.Course!).ToList();   // null = student not found
        }

        // implementation
        public async Task<(List<Student> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = db.Students.AsNoTracking();

            var total = await query.CountAsync();            // query 1: COUNT(*)

            var items = await query
                .OrderBy(s => s.Id)                          // always order before paging
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();                              // query 2: one page of rows

            return (items, total);
        }

        public Task<Student?> GetByUserIdAsync(int userId) =>
            db.Students.FirstOrDefaultAsync(s => s.UserId == userId);
    }
}
