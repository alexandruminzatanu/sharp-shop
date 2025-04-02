namespace SharpShop.Data
{
    public class GetProductRepository
    {
        public static string Get(string productName)
        {
            // Define file path
            string filePath = $"C:/Projects/SharpShop/sharp-shop/SharpShop.Data/DB/{productName}.json";

            // Read JSON string from the file
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);

                return jsonString;
            }
            else
            {
                return "";
            }
        }
    }
}

