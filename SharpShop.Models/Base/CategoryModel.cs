namespace SharpShop.Models.Base;

public class CategoryModel
{
    public CategoryModel() { }
    public CategoryModel(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public virtual ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}
