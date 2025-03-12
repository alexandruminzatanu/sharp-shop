namespace SharpShopBlueprints.Base
{
    public abstract class Product(int id, string name, string description, decimal price, int stock, Category category)
    {

        private string _name = string.Empty;

        public string Name
        {
            get => _name; set => _name = name.Length > 2 ? name : throw new ArgumentException("Name should have more than 2 chars");
        }

        public int Id { get; set; } = id;
        public string Description { get; set; } = description;
        public decimal Price { get; set; } = price;
        public int Stock { get; set; } = stock;

        // add category property to Product as Category class
        public Category Category { get; set; } = category;

        public abstract void ShowInfo();
        public override string ToString()
        {
            return $"{Name} - {Description}: ${Price} (Stock: {Stock})";
        }
    }
}
