using FluentValidation;
using ProniaOnion.Application.Dtos.Color;

namespace ProniaOnion.Application.Validators
{
    public class CreateColorDtoValidator : AbstractValidator<CreateColorDto>
    {
        public CreateColorDtoValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 50).WithMessage("Name max characters is 2-50")
                .Matches(@"^[a-zA-Z0-9\s]*$");
        }
    }
}
