using FluentValidation;
using Motocomplex.DTOs.BrandDTOs;

namespace Motocomplex.Utilities.Validators.BrandValidators
{
    public class BrandUpdateValidators : AbstractValidator<BrandUpdateDto>
    {
        public BrandUpdateValidators()
        {
            RuleFor(b => b.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 30).WithMessage("Name must be between 1 and 30 characters long.");
        }
    }
}
