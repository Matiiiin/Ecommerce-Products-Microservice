namespace Ecommerce.Product.Core.DTO.Product;

public record ProductResponseDTO(Guid ProductID, string ProductName, double? UnitPrice, int? QuantityInStock)
{

}