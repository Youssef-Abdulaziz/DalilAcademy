using DalilAcademy.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Data.Config
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => e.Id);
            // A student can't enroll in the same course twice
            builder.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();
            builder.HasOne(e => e.Student).WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId);
            builder.HasOne(e => e.Course).WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);
            builder.Property(e => e.EnrolledAt).HasDefaultValueSql("now()");
            builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);
        }
    }
}
