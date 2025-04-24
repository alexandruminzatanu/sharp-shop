using SharpShop.Models.Base;
using System.Text.Json;

namespace SharpShop.Data.FileRepositories
{
   public class PostProductRepository
    {
        // save a json to a file on disk
        public static string Save(Product product)
        {
            string jsonString = JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true });

            string filePath = $"C:/Projects/SharpShop/sharp-shop/SharpShop.Data/DB/{product.Id}.json";

            File.WriteAllText(filePath, jsonString);

            return $"JSON saved to {filePath}";

        }

    }
}
