using Ecommerce.Product.Core.DTO.Product;
using Ecommerce.Product.Core.ServiceContracts.Product;

namespace Ecommerce.Product.WebApi.MinimalApiEndpoints;

public static class ProductsMinimalApiRoutes
{
    public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var productsV1 = endpoints.NewVersionedApi("Products")
            .MapGroup("/api/v{version:apiVersion}/products")
            .HasApiVersion(1.0)
            .WithGroupName("v1")
            .WithTags("Products");
        
        productsV1.MapGet("/", async (IProductsServiceContract service) =>
        {
            var result = await service.GetProductsAsync();
            return Results.Ok(result);
        });

        
        
        
        
        
        
        productsV1.MapGet("/{productId:guid}", async (
            Guid productId,
            IProductsServiceContract service) =>
        {
            var result = await service.GetProductByIdAsync(productId);

            return result is null
                ? Results.NotFound()
                : Results.Ok(result);
        });

        
        
        
        
        
        
        
        productsV1.MapGet("/by-name/{name}", async (
            string name,
            IProductsServiceContract service) =>
        {
            var result = await service.GetProductByProductNameAsync(name);

            return result is null
                ? Results.NotFound()
                : Results.Ok(result);
        });

        
        
        
        
        
        
        
        productsV1.MapGet("/by-unit-price/{unitPrice:decimal}", async (
            decimal unitPrice,
            IProductsServiceContract service) =>
        {
            var result = await service.GetProductsByUnitPriceAsync(unitPrice);
            return Results.Ok(result ?? []);
        });

        
        
        
        
        
        
        productsV1.MapGet("/by-stock/{quantity:int}", async (
            int quantity,
            IProductsServiceContract service) =>
        {
            var result = await service.GetProductsByQuantityInStockAsync(quantity);
            return Results.Ok(result ?? []);
        });

        
        
        
        
        
        
        
        // Protected writes
        productsV1.MapPost("/", async (
            ProductAddRequestDTO request,
            IProductsServiceContract service) =>
        {
            var result = await service.AddProductAsync(request);

            return result is null
                ? Results.Problem(statusCode: StatusCodes.Status500InternalServerError)
                : Results.Created("/api/products", result);
        });

        
        
        
        
        
        
        
        productsV1.MapPut("/{productId:guid}", async (
            Guid productId,
            ProductUpdateRequestDTO request,
            IProductsServiceContract service) =>
        {
            var result = await service.UpdateProductAsync(productId, request);
            return Results.Ok(result);
        });

        
        
        
        
        
        
        productsV1.MapDelete("/{productId:guid}", async (
            Guid productId,
            IProductsServiceContract service) =>
        {
            await service.DeleteProductAsync(productId);
            return Results.NoContent();
        });
        
        
        return endpoints;
    }

}