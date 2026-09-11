namespace Ecommerce.Product.Core.Domain.RepositoryContracts.Products;

public interface IProductGetterRepository
{
    public Task<List<Entities.Product>?> GetAllProductsAsync();
    public Task<Entities.Product?> GetProductByProductId(Guid productId);
    
    public Task<Entities.Product?> GetProductByProductNameAsync(string productName);
    
    public Task<List<Entities.Product>?> GetProductsByUnitPriceAsync(decimal price);
    
    public Task<List<Entities.Product>?> GetProductsByQuantityInStockAsync(int quantity);

}