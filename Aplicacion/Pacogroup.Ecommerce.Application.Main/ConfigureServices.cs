using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pacogroup.Ecommerce.Application.Interfaces;

namespace Pacogroup.Ecommerce.Application.Main
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICostumersApplication, CustomersApplication>();
            services.AddAutoMapper(config =>
            {
                config.AddMaps(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}