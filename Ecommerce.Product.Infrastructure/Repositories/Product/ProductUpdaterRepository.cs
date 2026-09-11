using Dapper;
using Ecommerce.Product.Core.Domain.RepositoryContracts.Products;
using Ecommerce.Product.Core.ServiceContracts.Database.Dapper;

namespace Ecommerce.Product.Infrastructure.Repositories.Product;

public class ProductUpdaterRepository : IProductUpdaterRepository
{
    private readonly IDapperDbContext _db;

    public ProductUpdaterRepository(IDapperDbContext db)
    {
        _db = db;
    }
    public async Task<Core.Domain.Entities.Product?> UpdateProductAsync(Core.Domain.Entities.Product product)
    {
        string sql = @"
        UPDATE ""Products""
        SET ""ProductName"" = @ProductName,
            ""UnitPrice"" = @UnitPrice,
            ""QuantityInStock"" = @QuantityInStock
        WHERE ""ProductID"" = @ProductID
        RETURNING ""ProductID"", ""ProductName"", ""UnitPrice"", ""QuantityInStock"";
    ";

        var updatedProduct = await _db.DbConnection.QueryFirstOrDefaultAsync<Core.Domain.Entities.Product>(sql, product);

        return updatedProduct;
    }
}