using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BazaR.HealthChecks
{
    public class AccountControllerHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientBuilder;
        public AccountControllerHealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientBuilder = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _httpClientBuilder.CreateClient();
                var response = await client.GetAsync("/Account/Login");
                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy("Account controller works");
                }
                return HealthCheckResult.Degraded("Account controller do not works");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Totaly unhealthy");
            }
        }
    }
}
