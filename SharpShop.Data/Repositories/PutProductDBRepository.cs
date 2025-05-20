using SharpShop.Models.Base;

namespace SharpShop.Data.Repositories
{
   public class PutProductDB
    {
       async public static Task<Product> Update(Product product)
        {
            using (var context = new SharpShopContext())
            {
                var existingProduct = await context.Products.FindAsync(product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Stock = product.Stock;
                    await context.SaveChangesAsync();
                    return existingProduct;
                }
                else
                {
                    throw new Exception("Product not found");
                }
            }


        }

    }
}
