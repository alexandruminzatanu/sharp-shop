using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SharpShop.Data.SQLContext;

namespace SharpShop.Data;

// Enables 'dotnet ef' design-time creation of SharpShopSQLContext
public class SharpShopSQLContextFactory : IDesignTimeDbContextFactory<SharpShopSQLContext>
{
    public SharpShopSQLContext CreateDbContext(string[] args)
    {
        // Determine base path (project directory of Data project)
        var basePath = Directory.GetCurrentDirectory();

        // Build configuration similar to runtime
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables();

        var configuration = configBuilder.Build();

        // If SQLPath missing, provide a safe local fallback to allow migration scaffolding
        if (string.IsNullOrWhiteSpace(configuration["SQLPath"]))
        {
            // LocalDB fallback (Windows) - adjust if needed
            configuration["SQLPath"] = "Server=(localdb)\\MSSQLLocalDB;Database=SharpShop;Trusted_Connection=True;TrustServerCertificate=True";
        }

        return new SharpShopSQLContext(configuration);
    }
}
