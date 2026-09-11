namespace Ecommerce.Product.Core.Domain.Entities;

public class Product
{
    public Guid ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? QuantityInStock { get; set; }
}