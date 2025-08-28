using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using SharpShop.Models.Base;

namespace SharpShop.Data.MongoContext;
public class SharpShopMongoContext : DbContext
{
    public DbSet<ProductModel> Products { get; init; }

    public SharpShopMongoContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductModel>().ToCollection("Products");
    }
}
