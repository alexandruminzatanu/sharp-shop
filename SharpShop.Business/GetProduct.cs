using SharpShop.Data.Repositories;
using SharpShop.Models.Base;

namespace SharpShop.Business
{
    public class GetProduct
    {
        public static Task<Product> Get(int productId)
        {
            return GetProductDB.Get(productId);
        }
    }
}
