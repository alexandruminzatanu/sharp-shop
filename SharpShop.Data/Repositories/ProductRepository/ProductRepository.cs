using SharpShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpShop.Data.Repositories.ProductRepository
{
    internal class ProductRepository: IProductReposity
    {
        public async Task Delete(int productId)
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
        public async Task<Product> Get(int productId)
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
        public async Task<Product> Save(Product product)
        {
            using (var context = new SharpShopContext())
            {
                context.Add(product);
                await context.SaveChangesAsync();
                return product;
            }
        }
        public async Task<Product> Update(Product product)
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
