using SharpShop.Models.Base;

namespace SharpShop.Data.Repositories.ProductsRepository;

public interface IProductsRepository
{
    Task<IEnumerable<ProductModel>> GetAll(string sortOrder = "asc", string name = "");
    Task<ProductModel> Get(int productId);
    Task<ProductModel> Save(ProductModel product);
    Task<ProductModel> Update(ProductModel product);
    Task Delete(int productId);
}

