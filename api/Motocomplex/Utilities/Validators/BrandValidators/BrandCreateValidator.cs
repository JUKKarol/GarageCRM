using FluentValidation;
using Motocomplex.DTOs.BrandDTOs;

namespace Motocomplex.Utilities.Validators.BrandValidators
{
    public class BrandCreateValidator : AbstractValidator<BrandCreateDto>
    {
        public BrandCreateValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 30).WithMessage("Name must be between 1 and 30 characters long.");
        }
    }
}
