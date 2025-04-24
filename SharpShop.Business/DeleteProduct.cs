using SharpShop.Data.FileRepositories;

namespace SharpShop.Business
{
    public class DeleteProduct
    {
        public static string Delete(string productId)
        {    
            return DeleteProductRepository.Delete(productId);
        }
    }
}
