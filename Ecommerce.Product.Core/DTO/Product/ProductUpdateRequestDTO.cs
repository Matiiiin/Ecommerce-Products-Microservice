namespace Ecommerce.Product.Core.DTO.Product;

public record ProductUpdateRequestDTO(string ProductName, decimal? UnitPrice, int? QuantityInStock)
{

}
public static class ProductUpdateRequestDTOExtensions
{
    /// <summary>
    /// Maps this DTO onto an existing Product entity, preserving its ProductID.
    /// Use this in the update flow once you've already fetched the target entity.
    /// </summary>
    public static Domain.Entities.Product ToProduct(this ProductUpdateRequestDTO productUpdateRequestDto, Domain.Entities.Product existingProduct)
    {

        existingProduct.ProductName = productUpdateRequestDto.ProductName;
        existingProduct.UnitPrice = productUpdateRequestDto.UnitPrice ?? 0;
        existingProduct.QuantityInStock = productUpdateRequestDto.QuantityInStock ?? 0;

        return existingProduct;
    }
    
}