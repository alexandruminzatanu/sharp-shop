using SharpShop.Data.Repositories;
using SharpShop.Models.Base;

namespace SharpShop.Business
{
    public class SaveProduct
    {
        public static Task<Product> Save(Product product)
        {     
            return PostProductDB.Save(product);
        }
    }
}
