using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NiceToes.ProductAPI.Data;

namespace NiceToes.ProductAPI.Extensions
{
    public class DbHealthCheck : IHealthCheck
    {
        private readonly AppDbContext _appDbContext;

        public DbHealthCheck(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = _appDbContext.Products.FirstAsync(x => x.ProductId == 1);
                return Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus, "An unhealthy result."));
            }
            catch (Exception e)
            {
                return Task.FromResult(HealthCheckResult.Healthy("A healthy result."));
            }
            
        }
    }
}
