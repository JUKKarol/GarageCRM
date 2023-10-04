using FluentValidation;
using Motocomplex.DTOs.EmployeeDTOs;

namespace Motocomplex.Utilities.Validators.EmployeeValidators
{
    public class EmployeeCreateValidator : AbstractValidator<EmployeeCreateDto>
    {
        public EmployeeCreateValidator()
        {
            RuleFor(e => e.Name)
               .NotEmpty().WithMessage("Name is required.")
               .Length(2, 40).WithMessage("Name must be between 2 and 40 characters long.");

            RuleFor(e => e.Surname)
               .NotEmpty().WithMessage("Surname is required.")
               .Length(2, 40).WithMessage("Surname must be between 2 and 40 characters long.");

            RuleFor(e => e.DateOfEmployment)
               .InclusiveBetween(new DateTime(1950, 1, 1), DateTime.UtcNow)
                .WithMessage("Incorrect date.");
        }
    }
}
