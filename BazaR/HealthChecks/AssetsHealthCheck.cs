using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BazaR.HealthChecks
{
    public class AssetsHealthCheck : IHealthCheck
    {
        private readonly IWebHostEnvironment _env;

        public AssetsHealthCheck(IWebHostEnvironment env)
        {
            _env = env;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath, "AssetsIconImg");

                if (Directory.Exists(path))
                {
                    return Task.FromResult(
                        HealthCheckResult.Healthy("Folder exists"));
                }

                return Task.FromResult(
                    HealthCheckResult.Unhealthy("Folder not found"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                    HealthCheckResult.Unhealthy(ex.Message));
            }
        }
    }
}
