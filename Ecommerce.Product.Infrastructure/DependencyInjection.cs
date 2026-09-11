using Ecommerce.Product.Core.ServiceContracts.Database.Dapper;
using Ecommerce.Product.Infrastructure.Dapper.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Product.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IDapperDbContext, DapperDbContext>();
        return services;
    }
}