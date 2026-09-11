using Asp.Versioning;
using Ecommerce.Product.Core;
using Ecommerce.Product.Infrastructure;
using Ecommerce.Product.WebApi.Middleware;
using Ecommerce.Product.WebApi.MinimalApiEndpoints;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCore();
builder.Services.AddInfrastructure();
builder.Services.AddScoped<ExceptionHandlingMiddleware>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ecommerce Product API",
        Version = "v1",
        Description = "Product management API"
    });

    // Ensures each Swagger document contains only its matching API version.
    options.DocInclusionPredicate((documentName, apiDescription) =>
        documentName == apiDescription.GroupName);
});

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = false;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Ecommerce Product API v1");

        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.MapProductsEndpoints();

app.Run();