using Dapper;
using Ecommerce.Product.Core.Domain.RepositoryContracts.Products;
using Ecommerce.Product.Core.ServiceContracts.Database.Dapper;

namespace Ecommerce.Product.Infrastructure.Repositories.Product;

public class ProductAdderRepository : IProductAdderRepository
{
    private readonly IDapperDbContext _db;

    public ProductAdderRepository(IDapperDbContext db)
    {
        _db = db;
    }

    public async Task<Core.Domain.Entities.Product?> AddProductAsync(Core.Domain.Entities.Product product)
    {
        string sql = $"""
                              INSERT INTO "Products" ("ProductID", "ProductName", "UnitPrice", "QuantityInStock")
                              VALUES (@ProductID, @ProductName, @UnitPrice, @QuantityInStock);
                      """;

        var rowsAffected = await _db.DbConnection.ExecuteAsync(sql, product);
        if (rowsAffected > 0)
        {
            return product;
        }

        return null;
    }
}