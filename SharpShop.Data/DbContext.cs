using Microsoft.EntityFrameworkCore;
using SharpShop.Models.Base;


public class SharpShopContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public string DbPath { get; }

    public SharpShopContext()
    {
        
        DbPath = "Server=D6MJY74\\SQLEXPRESS;Database=SharpShop;Trusted_Connection=True;User Id=sa;TrustServerCertificate=True";
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"{DbPath}");
}