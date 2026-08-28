using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pacogroup.Ecommerce.Application.Interfaces;
using Pacogroup.Ecommerce.Transversal.Common;

namespace Pacogroup.Ecommerce.Application.Main
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICostumersApplication, CustomersApplication>();
            services.AddScoped<IAuthApplication, AuthApplication>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddAutoMapper(config =>
            {
                config.AddMaps(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}