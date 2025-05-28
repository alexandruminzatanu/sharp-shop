using Microsoft.Extensions.Configuration;
using SharpShop.Models.Base;

namespace SharpShop.Data.Repositories.ProductRepository
{
    internal class ProductRepository(IConfiguration configuration): IProductRepository
    { private readonly IConfiguration _configuration = configuration;
        public async Task Delete(int productId)
        {
            using (var context = new SharpShopContext(_configuration))
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
        public async Task<ProductModel> Get(int productId)
        {
            using (var context = new SharpShopContext(_configuration))
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
        public async Task<ProductModel> Save(ProductModel product)
        {
            using (var context = new SharpShopContext(_configuration))
            {
                context.Add(product);
                await context.SaveChangesAsync();
                return product;
            }
        }
        public async Task<ProductModel> Update(ProductModel product)
        {
            using (var context = new SharpShopContext(_configuration))
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
