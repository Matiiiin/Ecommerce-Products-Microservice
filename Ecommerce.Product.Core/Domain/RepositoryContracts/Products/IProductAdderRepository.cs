namespace Ecommerce.Product.Core.Domain.RepositoryContracts.Products;

public interface IProductAdderRepository
{
  public Task<Entities.Product> AddProductAsync(Entities.Product product);
}