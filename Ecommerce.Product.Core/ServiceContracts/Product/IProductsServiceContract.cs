using Ecommerce.Product.Core.DTO.Product;

namespace Ecommerce.Product.Core.ServiceContracts.Product;

public interface IProductsServiceContract
{
    Task<List<ProductResponseDTO>> GetProductsAsync();
    Task<ProductResponseDTO?> GetProductByIdAsync(Guid productId);
    Task<ProductResponseDTO?> GetProductByProductNameAsync(string productName);
    Task<List<ProductResponseDTO>?> GetProductsByUnitPriceAsync(decimal unitPrice);
    Task<List<ProductResponseDTO>?> GetProductsByQuantityInStockAsync(int quantity);
    
    
    Task<ProductResponseDTO?> AddProductAsync(ProductAddRequestDTO productAddRequestDto);
    Task<ProductResponseDTO> UpdateProductAsync(Guid productId ,ProductUpdateRequestDTO productUpdateRequestDto);
    Task<bool> DeleteProductAsync(Guid productId);
    
    
}