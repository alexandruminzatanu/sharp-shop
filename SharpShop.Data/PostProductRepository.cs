namespace SharpShop.Data
{
   public class PostProduct
    {
        // save a json to a file on disk
        public static string Save(string productName)
        {
            // string jsonString = JsonSerializer.Serialize(person, new JsonSerializerOptions { WriteIndented = true });

            string filePath = $"C:/Projects/SharpShop/sharp-shop/SharpShop.Data/DB/{productName}.json";

            File.WriteAllText(filePath, productName);

            return $"JSON saved to {filePath}";

        }

    }
}
