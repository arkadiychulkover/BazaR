using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BazaR.HealthChecks
{
    public class AdminControllerHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientBuilder;
        public AdminControllerHealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientBuilder = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _httpClientBuilder.CreateClient();
                var response = await client.GetAsync("/Admin/Login");
                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy("Admin controller works");
                }
                return HealthCheckResult.Degraded("Admin controller do not works");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Totaly unhealthy");
            }
        }
    }
}
