using SharpShop.Models.Base;

namespace SharpShop.Data.Repositories
{
   public class PostProductDB
    {
       async public static Task<Product> Save(Product product)
        {
            using (var context = new SharpShopContext())
            {
                context.Add(product);
                await context.SaveChangesAsync();
                return product;
            }
        }

    }
}
