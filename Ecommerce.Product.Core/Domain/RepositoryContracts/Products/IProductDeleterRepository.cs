namespace Ecommerce.Product.Core.Domain.RepositoryContracts.Products;

public interface IProductDeleterRepository
{
    public Task<bool> DeleteProductAsync(Guid productId);

}