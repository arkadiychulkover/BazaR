using BazaR.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BazaR.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly AppDbContext _context;

        public DatabaseHealthCheck(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var checks = new Dictionary<string, bool>
                {
                    ["Categories"] = await _context.Categories.AnyAsync(cancellationToken),
                    ["Items"] = await _context.Items.AnyAsync(cancellationToken)
                };

                var failed = checks.Where(x => !x.Value).Select(x => x.Key).ToList();

                if (failed.Count == 0)
                {
                    return HealthCheckResult.Healthy("All critical tables OK");
                }

                return HealthCheckResult.Degraded(
                    $"Some tables are empty or unavailable: {string.Join(", ", failed)}");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Database error: {ex.Message}");
            }
        }
    }
}