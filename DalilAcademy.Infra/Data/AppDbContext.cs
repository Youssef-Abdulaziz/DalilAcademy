using DalilAcademy.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<User> Users => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Finds every IEntityTypeConfiguration<T> in this project
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor { Id = 1, FullName = "Ahmad Saleh" },
                new Instructor { Id = 2, FullName = "Lina Haddad" });

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "C# Fundamentals", Price = 50, InstructorId = 1 },
                new Course { Id = 2, Title = "ASP.NET Core Web API", Price = 80, InstructorId = 1 },
                new Course { Id = 3, Title = "PostgreSQL Essentials", Price = 60, InstructorId = 2 });

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FullName = "Youssef", Email = "Youssef@gmail.com" },
                new Student { Id = 2, FullName = "Alex", Email = "Alex@gmail.com" });

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "admin@dalil.jo", Role = Roles.Admin, PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123") } );
                

        }
        
    }
}
