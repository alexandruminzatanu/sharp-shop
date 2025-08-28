using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharpShop.Data.SQLContext;
using SharpShop.Models.Base;

namespace SharpShop.Data.Repositories.ProductsRepository;

public class ProductsSQLRepository : IProductsRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ProductsSQLRepository> _logger;

    public ProductsSQLRepository(IConfiguration configuration, ILogger<ProductsSQLRepository> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductModel>> GetAll(string sortOrder = "asc", string name = "")
    {
        // Validate input early
        if (!string.IsNullOrWhiteSpace(sortOrder) && sortOrder.ToLower() is not ("asc" or "desc"))
        {
            throw new ArgumentException("Invalid sortOrder. Allowed values are 'asc' or 'desc'.", nameof(sortOrder));
        }

        using var context = new SharpShopSQLContext(_configuration);
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var query = context.Products.AsQueryable();
            query = (sortOrder?.ToLower() == "desc") ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            var products = await query.ToListAsync();
            await transaction.CommitAsync();
            return products;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Failed to get products. sortOrder={SortOrder}, nameFilter={Name}", sortOrder, name);
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

            await transaction.RollbackAsync();
            _logger.LogWarning("Product not found. id={ProductId}", productId);
            throw new KeyNotFoundException($"Product with id {productId} not found");
        }
        catch (KeyNotFoundException)
        {
            // Already logged; rethrow specific not-found exception
            throw;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Failed to get product. id={ProductId}", productId);
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
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Database update error while saving product {@Product}", product);
            throw;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Unexpected error while saving product {@Product}", product);
            throw new DbUpdateException("Failed to save product.", ex);
        }
    }

    public async Task<ProductModel> Update(ProductModel product)
    {
        using var context = new SharpShopSQLContext(_configuration);
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var existingProduct = await context.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                await transaction.RollbackAsync();
                _logger.LogWarning("Product not found for update. id={ProductId}", product.Id);
                throw new KeyNotFoundException($"Product with id {product.Id} not found");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return existingProduct;
        }
        catch (KeyNotFoundException)
        {
            // Already logged; rethrow specific not-found exception
            throw;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Concurrency error while updating product id={ProductId}", product.Id);
            throw;
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Database update error while updating product id={ProductId}", product.Id);
            throw;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Unexpected error while updating product id={ProductId}", product.Id);
            throw new DbUpdateException("Failed to update product.", ex);
        }
    }

    public async Task Delete(int productId)
    {
        using var context = new SharpShopSQLContext(_configuration);
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var product = await context.Products.FindAsync(productId);
            if (product == null)
            {
                await transaction.RollbackAsync();
                _logger.LogWarning("Product not found for delete. id={ProductId}", productId);
                throw new KeyNotFoundException($"Product with id {productId} not found");
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (KeyNotFoundException)
        {
            // Already logged; rethrow specific not-found exception
            throw;
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Database update error while deleting product id={ProductId}", productId);
            throw;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Unexpected error while deleting product id={ProductId}", productId);
            throw new DbUpdateException("Failed to delete product.", ex);
        }
    }
}

