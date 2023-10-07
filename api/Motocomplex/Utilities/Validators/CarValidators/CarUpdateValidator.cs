using FluentValidation;
using Motocomplex.DTOs.CarDTOs;

namespace Motocomplex.Utilities.Validators.CarValidators
{
    public class CarUpdateValidator : AbstractValidator<CarUpdateDto>
    {
        public CarUpdateValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Id must not be empty.");

            RuleFor(c => c.Engine)
                .NotEmpty().WithMessage("Engine must not be empty.")
                .InclusiveBetween(100, 12000).WithMessage("Engine must be in the range of 100 to 12000 cm3.");

            RuleFor(c => c.RegistrationNumber)
                .NotEmpty().WithMessage("Registration number must not be empty.")
                .Length(3, 10).WithMessage("Registration number must be between 3 and 10 characters long.");

            RuleFor(c => c.Vin)
                .NotEmpty().WithMessage("Vin must not be empty.")
                .Length(11, 17).WithMessage("Vin must be between 11 and 17 characters long.");

            RuleFor(c => c.yearOfProduction)
                .InclusiveBetween(1900, DateTime.UtcNow.Year + 1).WithMessage("Incorrect year.");

            RuleFor(c => c.ModelId)
                .NotEmpty().WithMessage("Model Id must not be empty.");
        }
    }
}