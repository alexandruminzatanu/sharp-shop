using SharpShop.Models.Base;
namespace SharpShop.Business.Product
{
    public interface IProductService
    {
        Task<ProductModel> Get(int productId);
        Task<ProductModel> Save(ProductModel product);
        Task<ProductModel> Update(ProductModel product);
        Task Delete(int productId);
    }
}
