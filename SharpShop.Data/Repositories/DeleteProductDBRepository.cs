namespace SharpShop.Data.Repositories
{
   public class DeleteProductDB
    {
       async public static Task Delete(int productId)
        {
            using (var context = new SharpShopContext())
            {
                var product = await context.Products.FindAsync(productId);
                if (product != null)
                {
                    context.Products.Remove(product);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Product not found");
                }

            }


        }

    }
}
