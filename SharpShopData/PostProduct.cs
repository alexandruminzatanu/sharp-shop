namespace SharpShopData
{
    class PostProduct
    {
        // save a json to a file on disk
        public void Save(string product, string productName)
        {
           // string jsonString = JsonSerializer.Serialize(person, new JsonSerializerOptions { WriteIndented = true });

            string filePath = $"{productName}.json";

            File.WriteAllText(filePath, product);

            Console.WriteLine($"JSON saved to {filePath}");

        }

    }
}
