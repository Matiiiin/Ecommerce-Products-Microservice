using Ecommerce.Product.Core.Domain.RepositoryContracts.Products;
using Ecommerce.Product.Core.DTO.Product;
using Ecommerce.Product.Core.ServiceContracts.Product;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Product.Core.Services.Product;

public class ProductsService : IProductsServiceContract
{
    private readonly ILogger<ProductsService> _logger;
    private readonly IValidator<ProductAddRequestDTO> _addValidator;
    private readonly IValidator<ProductUpdateRequestDTO> _updateValidator;
    private readonly IProductAdderRepository _productAdderRepository;
    private readonly IProductGetterRepository _productGetterRepository;
    private readonly IProductUpdaterRepository _productUpdaterRepository;
    private readonly IProductDeleterRepository _productDeleterRepository;

    public ProductsService(
        ILogger<ProductsService> logger,
        IValidator<ProductAddRequestDTO> addValidator,
        IValidator<ProductUpdateRequestDTO> updateValidator,
        IProductAdderRepository productAdderRepository,
        IProductGetterRepository productGetterRepository,
        IProductUpdaterRepository productUpdaterRepository,
        IProductDeleterRepository productDeleterRepository
        )
        
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _addValidator = addValidator ?? throw new ArgumentNullException(nameof(addValidator));
        _updateValidator = updateValidator ?? throw new ArgumentNullException(nameof(updateValidator));
        _productAdderRepository = productAdderRepository;
        _productGetterRepository = productGetterRepository;
        _productUpdaterRepository = productUpdaterRepository;
        _productDeleterRepository = productDeleterRepository;
    }

    public async Task<List<ProductResponseDTO>> GetProductsAsync()
    {
            var products = await _productGetterRepository.GetAllProductsAsync();
            return products!.ToProductResponseDTOs();

    }

    public async Task<ProductResponseDTO?> GetProductByIdAsync(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("Product ID cannot be empty.", nameof(productId));

        var product =  await _productGetterRepository.GetProductByProductId(productId);
        if (product == null)
        {
            throw new KeyNotFoundException($"No product found with id '{productId}'.");
        }
        return product.ToProductResponseDTO();
    }

    public async Task<ProductResponseDTO?> GetProductByProductNameAsync(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name cannot be null or empty.", nameof(productName));

        var product =  await _productGetterRepository.GetProductByProductNameAsync(productName);
        if (product == null)
        {
            throw new KeyNotFoundException($"No product found with product name '{productName}'.");
        }
        return product.ToProductResponseDTO();
    }

    public async Task<List<ProductResponseDTO>?> GetProductsByUnitPriceAsync(decimal unitPrice)
    {
        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));

        var products = await _productGetterRepository.GetProductsByUnitPriceAsync(unitPrice);
        if (products == null || products.Count == 0)
        {
            throw new KeyNotFoundException($"No product found with unit price '{unitPrice}'.");
        }
        return products.ToProductResponseDTOs();
    }

    public async Task<List<ProductResponseDTO>?> GetProductsByQuantityInStockAsync(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Quantity in stock cannot be negative.", nameof(quantity));

        var products = await _productGetterRepository.GetProductsByQuantityInStockAsync(quantity);
        if (products == null || products.Count == 0)
        {
            throw new KeyNotFoundException($"No product found with quantity in stock '{quantity}'.");
        }
        return products.ToProductResponseDTOs();
    }

    public async Task<ProductResponseDTO?> AddProductAsync(ProductAddRequestDTO productAddRequestDto)
    {
        if (productAddRequestDto is null)
            throw new ArgumentNullException(nameof(productAddRequestDto), "Product data cannot be null.");

        ValidationResult validationResult = await _addValidator.ValidateAsync(productAddRequestDto);
        if (!validationResult.IsValid)
        {
            string errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validation failed while adding product: {errors}", validationResult.Errors);
        }
            var existingProduct = await _productGetterRepository.GetProductByProductNameAsync(productAddRequestDto.ProductName);
            if (existingProduct is not null)
                throw new InvalidOperationException($"A product named '{productAddRequestDto.ProductName}' already exists.");

            Domain.Entities.Product productEntity = productAddRequestDto.ToProduct();

            Domain.Entities.Product? addedProduct = await _productAdderRepository.AddProductAsync(productEntity);
            if (addedProduct is null)
                throw new ApplicationException("The product could not be added. Please try again.");

            return addedProduct.ToProductResponseDTO();
    }

    public async Task<ProductResponseDTO> UpdateProductAsync(Guid productId , ProductUpdateRequestDTO productUpdateRequestDto)
    {
        if (productUpdateRequestDto is null)
            throw new ArgumentNullException(nameof(productUpdateRequestDto), "Product data cannot be null.");
        if (productId == Guid.Empty)
            throw new ArgumentNullException("productId", "productId cannot be null.");
        

        ValidationResult validationResult = await _updateValidator.ValidateAsync(productUpdateRequestDto);
        if (!validationResult.IsValid)
        {
            string errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validation failed while updating product: {errors}", validationResult.Errors);
        }

            // See note below: matching by name is a stand-in until ProductID exists on this DTO.
            Domain.Entities.Product? existingProduct = await _productGetterRepository.GetProductByProductId(productId);
            if (existingProduct is null)
                throw new KeyNotFoundException($"No product found with id '{productId}' to update.");

            var updatedProduct = productUpdateRequestDto.ToProduct(existingProduct);

            Domain.Entities.Product? result = await _productUpdaterRepository.UpdateProductAsync(updatedProduct);
            if (result is null)
                throw new ApplicationException("The product could not be updated. Please try again.");

            return result!.ToProductResponseDTO();
    }

    public async Task<bool> DeleteProductAsync(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("Product ID cannot be empty.", nameof(productId));

        Domain.Entities.Product? existingProduct = await _productGetterRepository.GetProductByProductId(productId);
        if (existingProduct is null)
            throw new KeyNotFoundException($"No product found with ID '{productId}' to delete.");

        bool deletedProduct = await _productDeleterRepository.DeleteProductAsync(productId);
        if (deletedProduct is false)
            throw new ApplicationException("The product could not be deleted. Please try again.");

        return deletedProduct;
    }
}