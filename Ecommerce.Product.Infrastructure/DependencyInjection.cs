using Ecommerce.Product.Core.Domain.RepositoryContracts.Products;
using Ecommerce.Product.Core.ServiceContracts.Database.Dapper;
using Ecommerce.Product.Infrastructure.Dapper.Database;
using Ecommerce.Product.Infrastructure.Repositories.Product;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Product.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDapperDbContext, DapperDbContext>();
        services.AddScoped<IProductAdderRepository, ProductAdderRepository>();
        services.AddScoped<IProductGetterRepository,ProductGetterRepository>();
        services.AddScoped<IProductDeleterRepository, ProductDeleterRepository>();
        services.AddScoped<IProductUpdaterRepository,ProductUpdaterRepository>();
        return services;
    }
}