using SharpShop.Data.Repositories;

namespace SharpShop.Business
{
    public class DeleteProduct
    {
        public static Task Delete(int productId)
        {    
            return DeleteProductDB.Delete(productId);
        }
    }
}
