using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Task_1.Services
{
    /// <summary>
    /// Service for using of IRatesService
    /// </summary>
    public class CacheHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _cacheTimer;

#pragma warning disable CS1591 
        public CacheHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }
#pragma warning restore CS1591

        /// <summary>
        /// Create Timer with ActualizeCache
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var period = TimeSpan.FromDays(1d);
            var nextDate = now.Add(period).Date;
            var dueTime = nextDate - now;
            
            // reset cache every midnight (UTC)
            _cacheTimer = new Timer(ActualizeCache, _scopeFactory, dueTime, period);
            
            return Task.CompletedTask;
        }

        private void ActualizeCache(object state)
        {
            var factory = (IServiceScopeFactory) state;
            using var scope = factory.CreateScope();
            var rates = scope.ServiceProvider.GetRequiredService<IRatesService>();
            rates.ActualizeRatesAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Stop Timer from StartAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cacheTimer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
