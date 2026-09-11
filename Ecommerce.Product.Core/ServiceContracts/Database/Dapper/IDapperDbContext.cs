using System.Data;

namespace Ecommerce.Product.Core.ServiceContracts.Database.Dapper;

public interface IDapperDbContext
{
    public IDbConnection DbConnection { get; set; }
}