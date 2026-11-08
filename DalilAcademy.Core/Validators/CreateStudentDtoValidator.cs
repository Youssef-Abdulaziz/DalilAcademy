using DalilAcademy.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilAcademy.Core.Validators
{
    public class CreateStudentDtoValidator  : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator() 
        {
            RuleFor(s => s.FullName).NotEmpty().WithMessage("Full name is required").MaximumLength(200).WithMessage("Full name must be at most 200 characters");

            RuleFor(s => s.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email is not valid").MaximumLength(200).WithMessage("Email must be at most 200 characters");
        }

    }
}
