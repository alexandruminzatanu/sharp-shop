using SharpShop.Models.Base;
using System.Text.Json;

namespace SharpShop.Data.FileRepositories
{
   public class PutProductRepository
    {
        // save a json to a file on disk
        public static string Put(Product product)
        {
            // Define file path
            string filePath = $"C:/Projects/SharpShop/sharp-shop/SharpShop.Data/DB/{product.Id}.json";

            // Read JSON string from the file
            if (File.Exists(filePath))
            {
                string jsonString = JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(filePath, jsonString);

                return $"JSON saved to {filePath}";
            }
            else
            {
                return "File doesnt exist";
            }

        }

    }
}
