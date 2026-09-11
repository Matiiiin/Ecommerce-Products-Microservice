namespace Ecommerce.Product.Core.DTO.Product;

public record ProductAddRequestDTO(string ProductName, decimal? UnitPrice, int? QuantityInStock)
{
}

public static class ProductAddRequestDTOExtensions
{
    public static Domain.Entities.Product ToProduct(this ProductAddRequestDTO productAddRequestDto)
    {
        return new Domain.Entities.Product
        {
            ProductID = Guid.NewGuid(),
            ProductName = productAddRequestDto.ProductName,
            UnitPrice = productAddRequestDto.UnitPrice ?? 0,
            QuantityInStock = productAddRequestDto.QuantityInStock ?? 0
        };
    }
}