using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DalilAcademy.Infra.Repository
{
    public class CourseRepository(AppDbContext db) : ICourseRepository
    {
        //private static readonly List<Course> _courses = [];
        public async Task<PagedResult<CourseResponseDto>> GetAllAsync(
    int page, int pageSize, decimal? minPrice, decimal? maxPrice)
        {
            IQueryable<Course> query = db.Courses.AsNoTracking();

            if (minPrice.HasValue)
                query = query.Where(c => c.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(c => c.Price <= maxPrice.Value);

            var total = await query.CountAsync();               // counts the filtered set

            var items = await query
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CourseResponseDto(c.Id, c.Title, c.Price, c.Instructor!.FullName, c.ImageUrl))
                .ToListAsync();

            return new PagedResult<CourseResponseDto>(items, page, pageSize, total);
        }
        public async Task<Course?> GetByIdAsync(int id)
        {
            return await db.Courses.Include(c => c.Instructor).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Course> CreateAsync(Course course)
        {
            try
            {
                db.Courses.Add(course);
                await db.SaveChangesAsync();
                await db.Entry(course).Reference(c => c.Instructor).LoadAsync();
                return course;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation }) 
            {
                throw new ConflictException("A course with this title already exists");
            }
            
        }
        public async Task UpdateAsync(Course updated)
        {
            db.Courses.Update(updated);
             await db.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await db.Courses.FindAsync(id);
            if (course is null)
                return false;

            db.Courses.Remove(course);
            await db.SaveChangesAsync();
            return true;
        }

    }
}
