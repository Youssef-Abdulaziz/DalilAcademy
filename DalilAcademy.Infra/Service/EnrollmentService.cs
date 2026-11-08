using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using DalilAcademy.Core.Mappings;
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
        public async Task<EnrollmentResponseDto> CreateAsync(CreateEnrollmentDto dto)
        {
            await validator.ValidateAndThrowAsync(dto);                      

            if (await studentRepository.GetByIdAsync(dto.StudentId) is null)
                throw new NotFoundException($"Student {dto.StudentId} was not found.");   

            if (await courseRepository.GetByIdAsync(dto.CourseId) is null)
                throw new NotFoundException($"Course {dto.CourseId} was not found.");     

            if (await enrollmentRepository.ExistsAsync(dto.StudentId, dto.CourseId))
                throw new ConflictException("The student is already enrolled in this course.");   

            var created = await enrollmentRepository.CreateAsync(dto.ToEntity());
            return created.ToDto();
        }
    }
}
