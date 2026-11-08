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
    public class EnrollmentRepository(AppDbContext db) : IEnrollmentRepository
    {
        public async Task<bool> ExistsAsync(int studentId, int courseId)
        {
            return await db.Enrollments.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }

        public async Task<Enrollment> CreateAsync(Enrollment enrollment)
        {
            db.Enrollments.Add(enrollment);
            await db.SaveChangesAsync();
            return enrollment;
        }
    }
}
