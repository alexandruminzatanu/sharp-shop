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

        // Configure Order-Product relationship (many-to-many)
        modelBuilder.Entity<OrderModel>()
            .HasMany(o => o.Products)
            .WithMany(p => p.Orders);

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

        // Seed Orders
        modelBuilder.Entity<OrderModel>().HasData(
            new OrderModel(1, "123 Main St", true) { Id = 1 },
            new OrderModel(2, "456 Oak Ave", false) { Id = 2 }
        );
    }
}


// dotnet ef migrations add FixModels --project SharpShop.Data --startup-project SharpShop.Data --output-dir Migrations