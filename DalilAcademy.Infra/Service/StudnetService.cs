using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Mappings;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using DalilAcademy.Core.Exceptions;

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Infra.Service
{
    public class StudentService(IStudentRepository studentRepository, IValidator<CreateStudentDto> validator) : IStudentService
    {
        // implementation
        public async Task<PagedResult<StudentResponseDto>> GetPagedAsync(int page, int pageSize)
        {
            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 1, 50);          // task 03: never more than 50

            var (items, total) = await studentRepository.GetPagedAsync(page, pageSize);

            return new PagedResult<StudentResponseDto>(
                items.Select(s => s.ToDto()).ToList(), page, pageSize, total);
        }

        public async Task<StudentResponseDto?> GetByIdAsync(int id)
        {
            var student = await studentRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Student {id} was not found");
            return student.ToDto();
        }

        public async Task<StudentResponseDto> CreateAsync(CreateStudentDto dto)
        {
            await validator.ValidateAndThrowAsync(dto);   

            if (await studentRepository.EmailExistsAsync(dto.Email))
                throw new ConflictException("A student with this email already exists.");   

            var created = await studentRepository.CreateAsync(dto.ToEntity());
            return created.ToDto();
        }

        public async Task UpdateAsync(int id, UpdateStudentDto dto)
        {
            var success = await studentRepository.UpdateAsync(id, dto.ToEntity(id));
            if (!success)
                throw new NotFoundException($"Student {id} was not found.");
        }

        public async Task DeleteAsync(int id)
        {
            var success = await studentRepository.DeleteAsync(id);
            if (!success)
                throw new NotFoundException($"Student {id} was not found.");
        }

        public async Task<List<CourseResponseDto>> GetCoursesAsync(int id)
        {
            var courses = await studentRepository.GetCoursesAsync(id)
                ?? throw new NotFoundException($"Student {id} was not found.");

            return courses
                .Select(c => new CourseResponseDto(c.Id, c.Title, c.Price, c.Instructor!.FullName))
                .ToList();
        }
    }
}
