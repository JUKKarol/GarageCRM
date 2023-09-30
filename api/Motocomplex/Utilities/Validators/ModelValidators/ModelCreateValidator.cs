using FluentValidation;
using Motocomplex.DTOs.ModelDTOs;

namespace Motocomplex.Utilities.Validators.ModelValidators
{
    public class ModelCreateValidator : AbstractValidator<ModelCreateDto>
    {
        public ModelCreateValidator()
        {
            RuleFor(m => m.brandId)
                .NotEmpty().WithMessage("Brand Id is required.");

            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 30).WithMessage("Name must be between 1 and 30 characters long.");
        }
    }
}
