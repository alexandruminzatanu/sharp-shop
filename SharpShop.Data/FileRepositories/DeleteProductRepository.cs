namespace SharpShop.Data.FileRepositories
{
    public class DeleteProductRepository
    {
        public static string Delete(string productId)
        {
            // Define file path
            string filePath = $"C:/Projects/SharpShop/sharp-shop/SharpShop.Data/DB/{productId}.json";

            // Read JSON string from the file
            if (File.Exists(filePath))
            {
                File.Delete(filePath);

                return $"DELETED: {filePath}";
            }
            else
            {
                return "File doesnt exist";
            }
        }
    }
}

