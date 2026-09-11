namespace Ecommerce.Product.Core.DTO.Product;

public record ProductResponseDTO(Guid ProductID, string ProductName, decimal? UnitPrice, int? QuantityInStock)
{

}
public static class ProductResponseDTOExtensions
{
    public static ProductResponseDTO ToProductResponseDTO(this Domain.Entities.Product product)
    {
        return new ProductResponseDTO(
            product.ProductID,
            product.ProductName,
            product.UnitPrice,
            product.QuantityInStock
        );
    }

    public static List<ProductResponseDTO> ToProductResponseDTOs(this IEnumerable<Domain.Entities.Product> products)
    {
        return products.Select(p => p.ToProductResponseDTO()).ToList();
    }
}