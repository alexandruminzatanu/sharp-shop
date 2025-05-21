using Microsoft.Extensions.DependencyInjection;
using SharpShop.Business.Product;

// instatiate dependency injection
namespace SharpShop.Business
{
    public static class DependencyInjectionExtensions
    {

        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            return services;
        }
    }
}
