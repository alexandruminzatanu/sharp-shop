using SharpShop.Data.FileRepositories;
using SharpShop.Models.Base;

namespace SharpShop.Business
{
    public class UpdateProduct
    {
        public static string Update(Product product)
        {     
            return PutProductRepository.Put(product);
        }
    }
}
