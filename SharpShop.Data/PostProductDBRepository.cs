using SharpShop.Models.Base;

namespace SharpShop.Data
{
   public class PostProductDB
    {
       async public static void Save(Product product)
        {
            using (var context = new SharpShopContext())
            {
                context.Add(product);
                await context.SaveChangesAsync();
            }


        }

    }
}
