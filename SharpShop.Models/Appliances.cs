using SharpShop.Models.Base;

namespace SharpShop.Models
{
    public class Appliances(int id, string name, string description, decimal price, int stock, int warranty) : Product(id, name, description, price, stock)
    {
        public int Warranty { get; set; } = warranty;
     
    }
}
