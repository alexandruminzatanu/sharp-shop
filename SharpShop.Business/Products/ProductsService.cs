using SharpShop.Models.Base;
using SharpShop.Data.Repositories.ProductsRepository;

namespace SharpShop.Business.Products;

internal class ProductsService(IProductsRepository productsRepository) : IProductsService
{
    private readonly IProductsRepository _productsRepository = productsRepository;

    public Task<IEnumerable<ProductModel>> GetAll(string sortOrder = "asc", string name = "")
    {
        sortOrder = (sortOrder ?? "asc").Trim().ToLowerInvariant();
        sortOrder = sortOrder == "desc" ? "desc" : "asc";
        name = name?.Trim() ?? string.Empty;

        return _productsRepository.GetAll(sortOrder, name);
    }

    public Task<ProductModel> Get(int productId)
    {
        return _productsRepository.Get(productId);
    }
    public Task<ProductModel> Save(ProductModel product)
    {
        return _productsRepository.Save(product);
    }
    public Task<ProductModel> Update(ProductModel product)
    {
        return _productsRepository.Update(product);
    }
    public Task Delete(int productId)
    {
        return _productsRepository.Delete(productId);
    }
}

