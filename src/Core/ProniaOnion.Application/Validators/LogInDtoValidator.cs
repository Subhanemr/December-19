using FluentValidation;
using ProniaOnion.Application.Dtos.Account;

namespace ProniaOnion.Application.Validators
{
    public class LogInDtoValidator : AbstractValidator<LoginDto>
    {
        public LogInDtoValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage("Username or Email must be entered mutled")
                .Length(1, 255).WithMessage("It should be between 1 and 25 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must be entered mutled")
                .MinimumLength(6).WithMessage("Password should be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one numeric digit");
        }
    }
}
