using SharpShop.Data.Repositories;
using SharpShop.Models.Base;

namespace SharpShop.Business
{
    public class UpdateProduct
    {
        public static Task<Product> Update(Product product) {

            return PutProductDB.Update(product);
        }
    }
}
