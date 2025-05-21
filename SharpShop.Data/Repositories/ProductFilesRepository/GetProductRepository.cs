namespace SharpShop.Data.Repositories.ProductFilesRepository
{
    public class GetProductRepository
    {
        public static string Get(string productId)
        {
            // Define file path
            string filePath = $"C:/Projects/SharpShop/sharp-shop/SharpShop.Data/DB/{productId}.json";

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

