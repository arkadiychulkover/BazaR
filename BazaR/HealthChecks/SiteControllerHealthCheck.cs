using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BazaR.HealthChecks
{
    public class SiteControllerHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientBuilder;
        public SiteControllerHealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientBuilder = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) 
        {
            try 
            { 
                var client = _httpClientBuilder.CreateClient();
                var response = await client.GetAsync("/SiteController/Index");
                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy("Site controller works");
                }
                return HealthCheckResult.Degraded("Site controller do not works");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Totaly unhealthy");
            }
        }
    }
}
