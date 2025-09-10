using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharpShop.Models.Base;

namespace SharpShop.Data.SQLContext;

public class SharpShopSQLContext(IConfiguration configuration) : DbContext
{
    private readonly IConfiguration _configuration = configuration;
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<OrderModel> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"{_configuration.GetValue<string>("SQLPath")}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Category-Product relationship (one-to-many)
        modelBuilder.Entity<ProductModel>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        // Configure Order-Product relationship (many-to-many) using join table
        modelBuilder.Entity<OrderModel>()
            .HasMany(o => o.Products)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>("OrderModelProductModel",
                j => j.HasOne<ProductModel>().WithMany().HasForeignKey("ProductsId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<OrderModel>().WithMany().HasForeignKey("OrdersId").OnDelete(DeleteBehavior.Cascade));

        // Ignore the helper ProductsIds (list of int) so EF doesn't try to map it directly
        modelBuilder.Entity<OrderModel>().Ignore(o => o.ProductsIds);

        // Seed data
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Categories
        modelBuilder.Entity<CategoryModel>().HasData(
            new CategoryModel(1, "Electronics", "Electronic devices and gadgets"),
            new CategoryModel(2, "Clothing", "Apparel and fashion items"),
            new CategoryModel(3, "Books", "Books and literature")
        );

        // Seed Products
        modelBuilder.Entity<ProductModel>().HasData(
            new ProductModel { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 999.99m, Stock = 10, CategoryId = 1 },
            new ProductModel { Id = 2, Name = "T-Shirt", Description = "Cotton t-shirt", Price = 19.99m, Stock = 50, CategoryId = 2 },
            new ProductModel { Id = 3, Name = "Programming Book", Description = "Learn C# programming", Price = 49.99m, Stock = 25, CategoryId = 3 },
            new ProductModel { Id = 4, Name = "Smartphone", Description = "Latest smartphone", Price = 699.99m, Stock = 15, CategoryId = 1 }
        );

        // Seed Orders (ProductsIds is ignored by EF, used only for code clarity)
        modelBuilder.Entity<OrderModel>().HasData(
            new OrderModel(1, "123 Main St", true, new List<int> { 1, 2 }) { Id = 1 },
            new OrderModel(2, "456 Oak Ave", false, new List<int> { 2, 3, 4 }) { Id = 2 }
        );

        // Seed join table via shadow entity (many-to-many links)
        modelBuilder.SharedTypeEntity<Dictionary<string, object>>("OrderModelProductModel")
            .HasData(
                // Order 1
                new { OrdersId = 1, ProductsId = 1 },
                new { OrdersId = 1, ProductsId = 2 },
                // Order 2
                new { OrdersId = 2, ProductsId = 2 },
                new { OrdersId = 2, ProductsId = 3 },
                new { OrdersId = 2, ProductsId = 4 }
            );
    }
}

//dotnet ef migrations add FixModels3 --project SharpShop.Data --startup-project SharpShop.ApiService --context SharpSh
// op.Data.SQLContext.SharpShopSQLContext --output-dir Migrations
//dotnet ef database update --project SharpShop.Data --startup-project SharpShop.ApiService --context SharpShop.Data.SQLContext.SharpShopSQLContext