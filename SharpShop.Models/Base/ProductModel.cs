namespace SharpShop.Models.Base;

// EF Core-friendly entity with parameterless constructor and explicit FK scalar.
public class ProductModel
{
    public ProductModel() { }

    // Convenience constructor retained (categoryId optional) for existing test code.
    public ProductModel(int id, string name, string description, decimal price, int stock, int categoryId = 0)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    // Foreign key scalar for Category navigation.
    public int? CategoryId { get; set; }
    public virtual CategoryModel? Category { get; set; }
    public virtual ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();
}
