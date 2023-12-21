using FluentValidation;
using ProniaOnion.Application.Dtos.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Validators
{
    internal class CreateColorDtoValidator : AbstractValidator<CreateColorDto>
    {
        public CreateColorDtoValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name max characters is 50")
                .MinimumLength(1).WithMessage("Name min characters is 1")
                .Matches(@"^[a-zA-Z0-9\s]*$");
        }
    }
}
