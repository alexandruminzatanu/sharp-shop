using SharpShop.Models.Base;
namespace SharpShop.Business.Products
{
    public interface IProductsService
    {
        Task<ProductModel> Get(int productId);
        Task<IEnumerable<ProductModel>> GetAll(string sortOrder = "asc", string name = "");
        Task<ProductModel> Save(ProductModel product);
        Task<ProductModel> Update(ProductModel product);
        Task Delete(int productId);
    }
}
