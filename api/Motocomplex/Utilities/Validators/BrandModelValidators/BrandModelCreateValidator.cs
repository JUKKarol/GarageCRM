using FluentValidation;
using Motocomplex.DTOs.BrandModelDTOs;

namespace Motocomplex.Utilities.Validators.BrandModelValidators
{
    public class BrandModelCreateValidator : AbstractValidator<BrandModelCreateDto>
    {
        public BrandModelCreateValidator()
        {
            RuleFor(b => b.BrandName)
                .NotEmpty().WithMessage("Brand is required.")
                .Length(1, 30).WithMessage("Name must be between 1 and 30 characters long.");

            RuleFor(m => m.ModelNames)
                .ForEach(modelName =>
                {
                    modelName.Length(1, 30).WithMessage("Model name must be between 1 and 30 characters long.");
                });
        }
    }
}