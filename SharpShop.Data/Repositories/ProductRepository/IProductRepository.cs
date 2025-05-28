using SharpShop.Models.Base;

namespace SharpShop.Data.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task Delete(int productId);
        Task<ProductModel> Update(ProductModel product);
        Task<ProductModel> Save(ProductModel product);
        Task<ProductModel> Get(int productId);
    }
}
