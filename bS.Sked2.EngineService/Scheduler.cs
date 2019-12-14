using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using bS.Sked2.Structure.Engine;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace bS.Sked2.EngineService
{
    internal class Scheduler : BackgroundService
    {
        private readonly ILogger<Scheduler> _logger;
        private readonly ILifetimeScope container;

        public Scheduler(ILogger<Scheduler> logger, ILifetimeScope container)
        {
            _logger = logger;
            this.container = container;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation($"Service running at time: {DateTimeOffset.Now}");
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}