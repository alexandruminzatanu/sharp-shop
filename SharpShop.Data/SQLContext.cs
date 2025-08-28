using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharpShop.Models.Base;

namespace SharpShop.Data.SQLContext;
public class SharpShopSQLContext(IConfiguration configuration) : DbContext
{   private readonly IConfiguration _configuration = configuration;
    public DbSet<ProductModel> Products { get; set; }
 
    protected override void OnConfiguring(DbContextOptionsBuilder options) 
        => options.UseSqlServer($"{_configuration.GetValue<string>("SQLPath")}");
}