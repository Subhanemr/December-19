using FluentValidation;
using ProniaOnion.Application.Dtos.Tag;

namespace ProniaOnion.Application.Validators
{
    internal class CreateTagDtoValidator : AbstractValidator<CreateTagDto>
    {
        public CreateTagDtoValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name max characters is 50")
                .MinimumLength(1).WithMessage("Name min characters is 1")
                .Matches(@"^[a-zA-Z0-9\s]*$");
        }
    }
}
