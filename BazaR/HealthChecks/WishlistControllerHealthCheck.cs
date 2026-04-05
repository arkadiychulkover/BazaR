using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BazaR.HealthChecks
{
    public class WishlistControllerHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientBuilder;
        public WishlistControllerHealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientBuilder = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _httpClientBuilder.CreateClient();
                var response = await client.GetAsync("/Wishlist/Index");
                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy("Wishlist controller works");
                }
                return HealthCheckResult.Degraded("Wishlist controller do not works");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Wishlist unhealthy");
            }
        }
    }
}
