using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharpShop.Data.SQLContext;
using SharpShop.Models.Base;

namespace SharpShop.Data.Repositories.ProductsRepository;

public class ProductsSQLRepository(IConfiguration configuration) : IProductsRepository
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEnumerable<ProductModel>> GetAll(string sortOrder = "asc", string name = "")
    {
        using var context = new SharpShopSQLContext(_configuration);
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var productsQuery = context.Products.AsQueryable();
                productsQuery = sortOrder?.ToLower() == "desc" ?  productsQuery.OrderByDescending(p => p.Name) :productsQuery.OrderBy(p => p.Name);
            if (!string.IsNullOrEmpty(name))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(name));
            }

            var products = await productsQuery.ToListAsync();
            await transaction.CommitAsync();
            return products;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<ProductModel> Get(int productId)
    {
        using var context = new SharpShopSQLContext(_configuration);
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var product = await context.Products.FindAsync(productId);
            if (product != null)
            {
                await transaction.CommitAsync();
                return product;
            }
            else
            {
                await transaction.RollbackAsync();
                throw new Exception("Product not found");
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<ProductModel> Save(ProductModel product)
    {
        using var context = new SharpShopSQLContext(_configuration);
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            context.Add(product);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return product;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<ProductModel> Update(ProductModel product)
    {
        using var context = new SharpShopSQLContext(_configuration);
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var existingProduct = await context.Products.FindAsync(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return existingProduct;
            }
            else
            {
                await transaction.RollbackAsync();
                throw new Exception("Product not found");
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task Delete(int productId)
    {
        using var context = new SharpShopSQLContext(_configuration);
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var product = await context.Products.FindAsync(productId);
            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            else
            {
                await transaction.RollbackAsync();
                throw new Exception("Product not found");
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}

