using FluentValidation;
using Motocomplex.DTOs.CustomerDtos;

namespace Motocomplex.Utilities.Validators
{
    public class CustomerUpdateValidator :AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateValidator()
        {
            RuleFor(c => c.Id)
               .NotEmpty().WithMessage("Id is required.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(4, 40).WithMessage("Name must be between 4 and 40 characters long.");

            RuleFor(c => c.PhoneNumber)
               .NotEmpty().WithMessage("Phone number is required.")
               .Matches(@"^\d{9}$").WithMessage("Phone number must consist of 9 digits.");

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(c => c.Nip)
               .Matches(@"^\d{10}$").WithMessage("Nip must consist of 10 digits.");

            RuleFor(c => c.City)
               .Length(3, 30).WithMessage("City name must be between 3 and 30 characters long.");

            RuleFor(c => c.Address)
               .Length(3, 40).WithMessage("Address name must be between 3 and 30 characters long.");

            RuleFor(c => c.PostalCode)
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Invalid postal code format. Use XX-XXX format.");
        }
    }
}
