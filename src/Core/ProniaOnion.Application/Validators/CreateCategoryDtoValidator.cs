using FluentValidation;
using ProniaOnion.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Validators
{
    internal class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name max characters is 50")
                .MinimumLength(1).WithMessage("Name min characters is 1")
                .Matches(@"^[a-zA-Z0-9\s]*$");
        }
    }
}
