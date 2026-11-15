using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Enums;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Mappings;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Service
{
    public class EnrollmentService(
        IEnrollmentRepository enrollmentRepository,
        IStudentRepository studentRepository,
        ICourseRepository courseRepository,
        IValidator<CreateEnrollmentDto> validator) : IEnrollmentService
    {
        public async Task<List<EnrollmentResponseDto>> GetAllAsync()
        {
            var enrollments = await enrollmentRepository.GetAllAsync();
            return enrollments.Select(e => e.ToDto()).ToList();
        }

        public async Task<EnrollmentResponseDto> GetByIdAsync(int id)
        {
            var enrollment = await enrollmentRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Enrollment {id} was not found.");
            return enrollment.ToDto();
        }

        public async Task<EnrollmentResponseDto> CreateAsync(CreateEnrollmentDto dto)
        {
            await validator.ValidateAndThrowAsync(dto);

            if (await studentRepository.GetByIdAsync(dto.StudentId) is null)
                throw new NotFoundException($"Student {dto.StudentId} was not found.");

            var course = await courseRepository.GetByIdAsync(dto.CourseId)
                ?? throw new NotFoundException($"Course {dto.CourseId} was not found.");

            if (await enrollmentRepository.ExistsAsync(dto.StudentId, dto.CourseId))
                throw new ConflictException("The student is already enrolled in this course.");

            var enrollment = dto.ToEntity();
            enrollment.Status = course.Price == 0
                ? EnrollmentStatus.Paid          // free course: paid directly
                : EnrollmentStatus.Pending;

            var created = await enrollmentRepository.CreateAsync(enrollment);
            return created.ToDto();
        }

        public async Task UpdateAsync(int id, UpdateEnrollmentDto dto)
        {
            var enrollment = await enrollmentRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Enrollment {id} was not found.");

            if (enrollment.Status == EnrollmentStatus.Paid)
                throw new ConflictException("A paid enrollment can't be moved to another course.");

            var newCourse = await courseRepository.GetByIdAsync(dto.CourseId)
                ?? throw new NotFoundException($"Course {dto.CourseId} was not found.");

            if (await enrollmentRepository.ExistsAsync(enrollment.StudentId, dto.CourseId))
                throw new ConflictException("The student is already enrolled in this course.");

            enrollment.CourseId = newCourse.Id;
            enrollment.Course = newCourse;
            enrollment.Status = newCourse.Price == 0
                ? EnrollmentStatus.Paid
                : EnrollmentStatus.Pending;

            await enrollmentRepository.UpdateAsync(enrollment);
        }

        public async Task DeleteAsync(int id)
        {
            var enrollment = await enrollmentRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Enrollment {id} was not found.");

            if (enrollment.Status == EnrollmentStatus.Paid)
                throw new ConflictException("A paid enrollment can't be deleted.");

            await enrollmentRepository.DeleteAsync(enrollment);
        }

        public async Task<List<CourseResponseDto>> GetMyCoursesAsync(int studentId)
        {
            return await enrollmentRepository.GetPaidCoursesByStudentAsync(studentId);   // Paid only
        }

        
    }
}
