using FluentValidation;
using ProniaOnion.Application.Dtos.Account;

namespace ProniaOnion.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User Name must be entered mutled")
                .Length(1, 25).WithMessage("It should be between 1 and 25 characters");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must be entered mutled")
                .Length(1, 25).WithMessage("It should be between 1 and 25 characters");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname must be entered mutled")
                .Length(1, 25).WithMessage("It should be between 1 and 25 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email must be entered mutled")
                .Length(25, 255).WithMessage("It should be between 25 and 255 characters")
                .EmailAddress().WithMessage("Invalid email address")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password must be entered mutled")
                .MinimumLength(6).WithMessage("Password should be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one numeric digit");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password must be entered mutled")
                .Equal(x => x.Password).WithMessage("The password must be the same");
        }
    }
}
