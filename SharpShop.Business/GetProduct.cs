using SharpShop.Data;

namespace SharpShop.Business
{
    public class GetProduct
    {
        public static string Get(string productName)
        {
            return GetProductRepository.Get(productName);
        }
    }
}
