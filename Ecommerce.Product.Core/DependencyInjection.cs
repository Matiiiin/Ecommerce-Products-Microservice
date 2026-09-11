using Ecommerce.Product.Core.ServiceContracts.Product;
using Ecommerce.Product.Core.Services.Product;
using Ecommerce.Product.Core.Validators.DTO.Product;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Product.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IProductsServiceContract , ProductsService>();
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestDTOValidator>();

        return services;
    }
}