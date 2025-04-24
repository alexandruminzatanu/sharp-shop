using SharpShop.Data.FileRepositories;
using SharpShop.Models.Base;

namespace SharpShop.Business
{
    public class SaveProduct
    {
        public static string Save(Product product)
        {     // make controllers and services for every type of products you have
            // var device = new Electronics(0, "", "",100,1,new Models.Base.Category(0,"",""),2);
            return PostProductRepository.Save(product);
        }
    }
}
