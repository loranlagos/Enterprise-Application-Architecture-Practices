namespace Pacogroup.Ecommerce.Services.WebApi.Modules.HealthChecks
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("NorthwindConnection"), tags: new[] { "database" })
                .AddCheck<HealthCheckCustom>("HealthCheckCustom", tags: new[] { "custom" });
            services.AddHealthChecksUI().AddInMemoryStorage();

            return services;
        }
    }
}