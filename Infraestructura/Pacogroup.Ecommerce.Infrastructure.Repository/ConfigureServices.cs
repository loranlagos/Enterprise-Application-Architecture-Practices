using Microsoft.Extensions.DependencyInjection;
using Pacogroup.Ecommerce.Domain.Entity;
using Pacogroup.Ecommerce.Infrastructure.Data;
using Pacogroup.Ecommerce.Infrastructure.Interfaces;

namespace Pacogroup.Ecommerce.Infrastructure.Repository
{
    public static class ConfigureServices
    {
        /// <summary>
        /// Extiende la inyección de deopendencias para la capa de infraestructura
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfraestrutureServices(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<ICostumersRepository, CostumersRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}