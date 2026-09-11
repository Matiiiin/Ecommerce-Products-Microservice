namespace Ecommerce.Product.Core.Domain.RepositoryContracts.Products;

public interface IProductUpdaterRepository
{
    public Task<Entities.Product?> UpdateProductAsync(Entities.Product product);

}