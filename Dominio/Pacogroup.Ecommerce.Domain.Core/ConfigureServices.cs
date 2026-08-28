using Microsoft.Extensions.DependencyInjection;
using Pacogroup.Ecommerce.Domain.Interfaces;

namespace Pacogroup.Ecommerce.Domain.Core
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            // Inyeccion de la interfaz con su implementacion
            services.AddScoped<ICostumersDomain, CustomersDomain>();
            services.AddScoped<IUsersDomain, UsersDomain>();

            return services;
        }
    }
}