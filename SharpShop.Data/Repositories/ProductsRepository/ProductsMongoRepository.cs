// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using SharpShop.Data.MongoContext;
// using SharpShop.Models;
// using SharpShop.Models.Base;

// namespace SharpShop.Data.Repositories.ProductsRepository;

// internal class ProductsMongoRepository(SharpShopMongoContext sharpShopMongoContext) : IProductsRepository
// {
//     private readonly SharpShopMongoContext _sharpShopMongoContext = sharpShopMongoContext;

//     public async Task<IEnumerable<ProductModel>> GetAll(string sortOrder = "asc", string name = "")
//     {
//         IQueryable<ProductModel> productsQuery = _sharpShopMongoContext.Products.AsQueryable();

//         if (!string.IsNullOrEmpty(name))
//         {
//             productsQuery = productsQuery.Where(p => p.Name.Contains(name));
//         }

//         if (sortOrder?.ToLower() == "desc")
//         {
//             productsQuery = productsQuery.OrderByDescending(p => p.Name);
//         }
//         else
//         {
//             productsQuery = productsQuery.OrderBy(p => p.Name);
//         }

//         return await Task.Run(() => productsQuery.AsNoTracking().AsEnumerable());
//     }

//     public async Task<ProductModel> Get(int productId)
//     {
//         var product = await _sharpShopMongoContext.Products.FindAsync(productId);
//         if (product != null)
//         {
//             return product;
//         }
//         else
//         {
//             throw new Exception("Product not found");
//         }
//     }

//     public async Task<ProductModel> Save(ProductModel product)
//     {
//         await _sharpShopMongoContext.Products.AddAsync(product);
//         await _sharpShopMongoContext.SaveChangesAsync();
//         return product;
//     }

//     public async Task<ProductModel> Update(ProductModel product)
//     {
//         var existingProduct = await _sharpShopMongoContext.Products.FindAsync(product.Id);
//         if (existingProduct != null)
//         {
//             existingProduct.Name = product.Name;
//             existingProduct.Description = product.Description;
//             existingProduct.Price = product.Price;
//             existingProduct.Stock = product.Stock;
//             await _sharpShopMongoContext.SaveChangesAsync();
//             return existingProduct;
//         }
//         else
//         {
//             throw new Exception("Product not found");
//         }
//     }

//     public async Task Delete(int productId)
//     {
//         var product = await _sharpShopMongoContext.Products.FindAsync(productId);
//         if (product != null)
//         {
//             _sharpShopMongoContext.Products.Remove(product);
//             await _sharpShopMongoContext.SaveChangesAsync();
//         }
//         else
//         {
//             throw new Exception("Product not found");
//         }
//     }
// }

