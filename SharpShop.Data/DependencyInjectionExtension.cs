using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpShop.Data.Repositories.ProductsRepository;

namespace SharpShop.Data;
public static class DependencyInjectionExtensions
{

    public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var useDb = configuration.GetValue<string>("useDb");
        if (useDb == "SQL")
        {
            services.AddTransient<IProductsRepository, ProductsSQLRepository>();
        }
        else if (useDb == "Mongo")
        {
            services.AddTransient<IProductsRepository, ProductsMongoRepository>();
        }
        else if (useDb == "File")
        {
            services.AddTransient<IProductsRepository, ProductsFileRepository>();
        }

        return services;
    }
}