using SharpShop.Models.Base;

namespace SharpShop.Models
{
    public class Electronics(int id, string name, string description, decimal price, int stock, int warranty) : ProductModel(id, name, description, price, stock)
    {
        public int Warranty { get; set; } = warranty;
    }
}
