namespace Ecommerce.Product.Core.DTO.Product;

public record ProductUpdateRequestDTO(string ProductName, double? UnitPrice, int? QuantityInStock)
{

}