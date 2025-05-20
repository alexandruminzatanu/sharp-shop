using SharpShop.Models.Base;
using System.Text.Json;

namespace SharpShop.Data.Repositories
{
   public class GetProductDB
    {
       async public static Task<Product> Get(int productId)
        {
            using (var context = new SharpShopContext())
            {
                var product = await context.Products.FindAsync(productId);
                if (product != null)
                {
                    return product;
                }
                else
                {
                    throw new Exception("Product not found");
                }
            }


        }

    }
}
