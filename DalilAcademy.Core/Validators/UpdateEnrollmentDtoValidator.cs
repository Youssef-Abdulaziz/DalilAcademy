using DalilAcademy.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Validators
{
    public class UpdateEnrollmentDtoValidator : AbstractValidator<UpdateEnrollmentDto>
    {
        public UpdateEnrollmentDtoValidator() 
        {
            RuleFor(e => e.CourseId)
            .GreaterThan(0).WithMessage("CourseId must be a positive number.");
        }
    }
}
