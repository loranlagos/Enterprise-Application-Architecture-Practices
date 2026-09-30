using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Pacogroup.Ecommerce.Services.WebApi.Modules.HealthChecks
{
    public class HealthCheckCustom : IHealthCheck
    {
        private readonly Random _random = new();
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var reponseTime = _random.Next(1, 300);

            if (reponseTime < 100)
            {
                return await Task.FromResult(HealthCheckResult.Healthy("Healthy result from HealthCheckCustom"));
            }
            else if (reponseTime < 200)
            {
                return await Task.FromResult(HealthCheckResult.Degraded("Degraded result from HealthCheckCustom"));
            }
            else
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy("Unhealthy result from HealthCheckCustom"));
            }
        }
    }
}
