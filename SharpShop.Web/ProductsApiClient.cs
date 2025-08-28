namespace SharpShop.Web;

public class ProductsApiClient(HttpClient httpClient)
{
    public async Task<ProductModel[]> GetProductsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<ProductModel>? products = [];

        await foreach (var product in httpClient.GetFromJsonAsAsyncEnumerable<ProductModel>("/api/products", cancellationToken))
        {
            if (product is not null)
            {
                products.Add(product);
            }
        }

        return products?.ToArray() ?? [];
    }
}

public record ProductModel(int Id, string Name, string Description, decimal Price, int Stock)
{
    
}
