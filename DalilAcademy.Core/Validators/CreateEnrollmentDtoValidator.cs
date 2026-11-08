using DalilAcademy.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CreateEnrollmentDtoValidator : AbstractValidator<CreateEnrollmentDto>
{
    public CreateEnrollmentDtoValidator()
    {
        RuleFor(e => e.StudentId)
            .GreaterThan(0).WithMessage("StudentId must be a positive number.");

        RuleFor(e => e.CourseId)
            .GreaterThan(0).WithMessage("CourseId must be a positive number.");
    }
}
