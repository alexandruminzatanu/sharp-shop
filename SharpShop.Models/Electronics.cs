using SharpShop.Models.Base;

namespace SharpShop.Models
{
    public class Electronics(int id, string name, string description, decimal price, int stock, Category category, int warranty) : Product(id, name, description, price, stock, category)
    {
        public int Warranty { get; set; } = warranty;
        public override void ShowInfo()
        {
            Console.WriteLine($"Electronics: {Name} - {Description}: ${Price} (Stock: {Stock})");
        }
    }
}
