using FluentValidation;
using ProniaOnion.Application.Dtos.Product;

namespace ProniaOnion.Application.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto> 
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name max characters is 100")
                .MinimumLength(1).WithMessage("Name min characters is 1");

            RuleFor(x => x.sku)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(10).WithMessage("SKU max characters is 10");

            RuleFor(x => x.price)
              .NotEmpty().WithMessage("Name is required")
              .LessThanOrEqualTo(999999.99m).WithMessage("Price must not be more than 999999.99")
              .GreaterThanOrEqualTo(10).WithMessage("Price must not be small be 10");

            RuleFor(x => x.description).MaximumLength(1000).WithMessage("Decription max characters is 1000");
        }
    }
}
