using FluentValidation;
using ProniaOnion.Application.Dtos.Categories;

namespace ProniaOnion.Application.Validators
{
    internal class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name max characters is 50")
                .MinimumLength(1).WithMessage("Name min characters is 1")
                .Matches(@"^[a-zA-Z0-9\s]*$");
        }
    }
}
