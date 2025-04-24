using SharpShop.Data.FileRepositories;

namespace SharpShop.Business
{
    public class GetProduct
    {
        public static string Get(string productId)
        {
            return GetProductRepository.Get(productId);
        }
    }
}
