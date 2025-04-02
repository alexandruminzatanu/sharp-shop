
using SharpShop.Data;

namespace SharpShop.Business
{
    public class SaveProduct
    {
        public static string Save(string product)
        {
            return PostProduct.Save(product);
        }
    }
}
