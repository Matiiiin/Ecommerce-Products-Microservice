namespace Ecommerce.Product.Core.DTO.Product;

public record ProductAddRequestDTO(string ProductName, double? UnitPrice, int? QuantityInStock)
{
}