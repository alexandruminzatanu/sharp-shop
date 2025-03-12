using SharpShopBlueprints.Base;

namespace SharpShopBlueprints
{
    public class Appliances(int id, string name, string description, decimal price, int stock, Category category, int warranty) : Product(id, name, description, price, stock, category)
    {
        public int Warranty { get; set; } = warranty;
        public override void ShowInfo()
        {
            Console.WriteLine($"Appliances: {Name} - {Description}: ${Price} (Stock: {Stock})");
        }
    }
}
