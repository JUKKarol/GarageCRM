using FluentValidation;
using Motocomplex.DTOs.EmployeeDTOs;

namespace Motocomplex.Utilities.Validators.EmployeeValidators
{
    public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdateDto>
    {
        public EmployeeUpdateValidator()
        {
            RuleFor(e => e.Id)
               .NotEmpty().WithMessage("Id is required.");

            RuleFor(e => e.Name)
               .NotEmpty().WithMessage("Name is required.")
               .Length(2, 40).WithMessage("Name must be between 2 and 40 characters long.");

            RuleFor(e => e.Surname)
               .NotEmpty().WithMessage("Surname is required.")
               .Length(2, 40).WithMessage("Surname must be between 2 and 40 characters long.");

            RuleFor(e => e.DateOfEmployment)
               .InclusiveBetween(new DateTime(1950, 1, 1), DateTime.UtcNow)
                .WithMessage("Incorrect date.");

            RuleFor(e => e.Role)
               .IsInEnum()
               .WithMessage("Incorrect role.");
        }
    }
}