using FluentValidation;
using Ecommerce.Product.Core.DTO.Product;

namespace Ecommerce.Product.Core.Validators.DTO.Product;

public class ProductUpdateRequestDTOValidator : AbstractValidator<ProductUpdateRequestDTO>
{
    public ProductUpdateRequestDTOValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(200).WithMessage("Product name must not exceed 200 characters.");

        RuleFor(x => x.UnitPrice)
            .NotNull().WithMessage("Unit price is required.")
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.")
            .When(x => x.UnitPrice.HasValue);

        RuleFor(x => x.QuantityInStock)
            .NotNull().WithMessage("Quantity in stock is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Quantity in stock cannot be negative.")
            .When(x => x.QuantityInStock.HasValue);
    }
}