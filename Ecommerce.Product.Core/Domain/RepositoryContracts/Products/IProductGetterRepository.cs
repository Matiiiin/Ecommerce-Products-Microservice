namespace Ecommerce.Product.Core.Domain.RepositoryContracts.Products;

public interface IProductGetterRepository
{
    public Task<List<Entities.Product>> GetAllProductsAsync();
    
    public Task<Entities.Product> GetProductByProductName(string productName);
    
    public Task<List<Entities.Product>> GetProductsByUniPrice(decimal price);
    
    public Task<List<Entities.Product>> GetProductsByQuantityInStock(int quantity);

}