// add dependency injection extension method

using Microsoft.Extensions.DependencyInjection;
using SharpShop.Data.Repositories.ProductRepository;

// instatiate dependency injection
namespace SharpShop.Data
{
    public static class DependencyInjectionExtensions
    {

        public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
