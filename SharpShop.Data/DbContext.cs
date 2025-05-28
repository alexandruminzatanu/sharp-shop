using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharpShop.Models.Base;


public class SharpShopContext(IConfiguration configuration) : DbContext
{   private readonly IConfiguration _configuration = configuration;
    public DbSet<ProductModel> Products { get; set; }
 
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"{_configuration.GetValue<string>("DbPath")}");
}