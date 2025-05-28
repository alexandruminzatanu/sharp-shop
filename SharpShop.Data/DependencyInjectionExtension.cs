using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpShop.Data.Repositories.ProductRepository;

namespace SharpShop.Data
{
    public static class DependencyInjectionExtensions
    {

        public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var useDb = configuration.GetValue<bool>("useDb");
            if (useDb) { services.AddTransient<IProductRepository, ProductRepository>(); }
            else
            {
                services.AddTransient<IProductRepository, ProductFileRepository>();
            }

            return services;
        }
    }
}
