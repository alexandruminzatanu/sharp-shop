using Microsoft.Extensions.Configuration;
using SharpShop.Models.Base;
using System.Text.Json;

namespace SharpShop.Data.Repositories.ProductsRepository;

internal class ProductsFileRepository(IConfiguration configuration) : IProductsRepository
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEnumerable<ProductModel>> GetAll(string sortOrder = "asc", string name = "")
    {
        var basePath = _configuration.GetValue<string>("FileStoragePath");
        if (!Directory.Exists(basePath))
        {
            throw new DirectoryNotFoundException($"Directory not found: {basePath}");
        }

        var productFiles = Directory.GetFiles(basePath, "*.json");
        var products = new List<ProductModel>();

        foreach (var file in productFiles)
        {
            string jsonString = await Task.Run(() => File.ReadAllText(file));
            var product = JsonSerializer.Deserialize<ProductModel>(jsonString);
            if (product != null)
            {
                products.Add(product);
            }
        }
        return products;
    }

    public async Task<ProductModel> Get(int productId)
    {
        var basePath = _configuration.GetValue<string>("FileStoragePath");
        string filePath = $"{basePath}/{productId}.json";
        if (File.Exists(filePath))
        {
            string jsonString = await Task.Run(() => File.ReadAllText(filePath));
            var product = JsonSerializer.Deserialize<ProductModel>(jsonString) ?? throw new InvalidOperationException("Deserialized product is null.");
            return product;
        }
        else
        {
            throw new FileNotFoundException($"Product file not found: {filePath}");
        }
    }
    public async Task<ProductModel> Save(ProductModel product)
    {
        var basePath = _configuration.GetValue<string>("FileStoragePath");
        string jsonString = JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true });
        string filePath = $"{basePath}/{product.Id}.json";
        await Task.Run(() => File.WriteAllText(filePath, jsonString));
        return product;

    }
    public async Task<ProductModel> Update(ProductModel product)
    {
        var basePath = _configuration.GetValue<string>("FileStoragePath");
        string filePath = $"{basePath}/{product.Id}.json";
        if (File.Exists(filePath))
        {
            string jsonString = JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true });
            await Task.Run(() => File.WriteAllText(filePath, jsonString));
            return product;
        }
        else
        {
            throw new FileNotFoundException($"Product file not found: {filePath}");
        }

    }
    
     public async Task Delete(int productId)
    {
        var basePath = _configuration.GetValue<string>("FileStoragePath");
        string filePath = $"{basePath}/{productId}.json";
        if (File.Exists(filePath))
        {
            await Task.Run(() => File.Delete(filePath));
        }
    }
}

