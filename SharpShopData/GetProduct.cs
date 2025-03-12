namespace SharpShopData
{
    class GetProduct
    {
        public string Get(string prductName)
        {
            // Define file path
            string filePath = $"{prductName}.json";

            // Read JSON string from the file
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);

                return jsonString;
            }
            else
            {
                Console.WriteLine("File not found!");
                return "";
            }
        }
    }
}

