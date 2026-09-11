using System.Data;
using Ecommerce.Product.Core.ServiceContracts.Database.Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Ecommerce.Product.Infrastructure.Dapper.Database;

public class DapperDbContext : IDapperDbContext
{
    public IDbConnection DbConnection { get; set; }

    public DapperDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        DbConnection = new NpgsqlConnection(connectionString);
    }
}