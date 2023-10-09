using FluentValidation;
using Motocomplex.DTOs.RepairDTOs;

namespace Motocomplex.Utilities.Validators.RepairValidators
{
    public class RepairUpdateValidator : AbstractValidator<RepairUpdateDto>
    {
        public RepairUpdateValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(r => r.Price)
                .NotEmpty().WithMessage("Price must not be empty.")
                .InclusiveBetween(100, 100000000).WithMessage("Price must be in the range of 100 to 1 000 000 00");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(5, 500).WithMessage("Description must be between 1 and 500 characters long.");

            RuleFor(r => r.CarId)
                .NotEmpty().WithMessage("Car Id is required.");

            RuleFor(r => r.CustomerId)
                .NotEmpty().WithMessage("Customer Id is required.");

            RuleFor(r => r.EmployeesIds)
                .NotEmpty().WithMessage("Employee Id is required.");
        }
    }
}