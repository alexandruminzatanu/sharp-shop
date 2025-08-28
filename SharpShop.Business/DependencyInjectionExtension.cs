using Microsoft.Extensions.DependencyInjection;
using SharpShop.Business.Products;

namespace SharpShop.Business
{
    public static class DependencyInjectionExtensions
    {

        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductsService, ProductsService>();
            return services;
        }
    }
}
