using System.Data;
using Dapper;
using Ecommerce.Product.Core.Domain.RepositoryContracts.Products;
using Ecommerce.Product.Core.ServiceContracts.Database.Dapper;

namespace Ecommerce.Product.Infrastructure.Repositories.Product;

public class ProductGetterRepository : IProductGetterRepository
{
    private readonly IDapperDbContext _db;

    public ProductGetterRepository(IDapperDbContext db)
    {
        _db = db;
    }

    public async Task<List<Core.Domain.Entities.Product>?> GetAllProductsAsync()
    {
        string sql = @"
            SELECT ""ProductID"", ""ProductName"", ""UnitPrice"", ""QuantityInStock""
            FROM ""Products"";
        ";

        var products = await _db.DbConnection.QueryAsync<Core.Domain.Entities.Product>(sql);

        return products.ToList();
    }

    public async Task<Core.Domain.Entities.Product?> GetProductByProductId(Guid productId)
    {
        string sql = @"
            SELECT ""ProductID"", ""ProductName"", ""UnitPrice"", ""QuantityInStock""
            FROM ""Products""
            WHERE ""ProductID"" = @ProductID;
        ";

        var product = await _db.DbConnection.QueryFirstOrDefaultAsync<Core.Domain.Entities.Product>(
            sql, new { ProductID = productId });

        return product;
    }

    public async Task<Core.Domain.Entities.Product?> GetProductByProductNameAsync(string productName)
    {
        string sql = @"
            SELECT ""ProductID"", ""ProductName"", ""UnitPrice"", ""QuantityInStock""
            FROM ""Products""
            WHERE ""ProductName"" = @ProductName;
        ";

        var product = await _db.DbConnection.QueryFirstOrDefaultAsync<Core.Domain.Entities.Product>(
            sql, new { ProductName = productName });

        return product;
    }

    public async Task<List<Core.Domain.Entities.Product>?> GetProductsByUnitPriceAsync(decimal price)
    {
        string sql = @"
            SELECT ""ProductID"", ""ProductName"", ""UnitPrice"", ""QuantityInStock""
            FROM ""Products""
            WHERE ""UnitPrice"" = @Price;
        ";

        var products = await _db.DbConnection.QueryAsync<Core.Domain.Entities.Product>(
            sql, new { Price = price });

        return products.ToList();
    }

    public async Task<List<Core.Domain.Entities.Product>?> GetProductsByQuantityInStockAsync(int quantity)
    {
        string sql = @"
            SELECT ""ProductID"", ""ProductName"", ""UnitPrice"", ""QuantityInStock""
            FROM ""Products""
            WHERE ""QuantityInStock"" = @Quantity;
        ";

        var products = await _db.DbConnection.QueryAsync<Core.Domain.Entities.Product>(
            sql, new { Quantity = quantity });

        return products.ToList();
    }
}