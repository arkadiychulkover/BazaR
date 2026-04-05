using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BazaR.HealthChecks
{
    public class AuthControllerHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientBuilder;
        public AuthControllerHealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientBuilder = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _httpClientBuilder.CreateClient();
                var response = await client.GetAsync("/Auth/Google");
                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy("Auth controller works");
                }
                return HealthCheckResult.Degraded("Auth controller do not works");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Totaly unhealthy");
            }
        }
    }
}
