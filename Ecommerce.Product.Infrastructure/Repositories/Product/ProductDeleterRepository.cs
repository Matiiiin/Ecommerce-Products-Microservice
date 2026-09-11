using Dapper;
using Ecommerce.Product.Core.Domain.RepositoryContracts.Products;
using Ecommerce.Product.Core.ServiceContracts.Database.Dapper;

namespace Ecommerce.Product.Infrastructure.Repositories.Product;

public class ProductDeleterRepository : IProductDeleterRepository
{
    private readonly IDapperDbContext _db;

    public ProductDeleterRepository(IDapperDbContext db)
    {
        _db = db;
    }
    public async Task<bool> DeleteProductAsync(Guid productId)
    {
        var sql = """DELETE FROM "Products" WHERE "ProductID" = @ProductID""";
        var rowsAffected =await _db.DbConnection.ExecuteAsync(sql, new { ProductID = productId });
        return rowsAffected > 0;
    }
}