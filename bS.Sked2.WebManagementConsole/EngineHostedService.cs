using bS.Sked2.Structure.Engine;
using bS.Sked2.WebManagementConsole.Hub;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace bS.Sked2.WebManagementConsole
{
    /// <summary>
    /// Real time hosted Engine Service
    /// </summary>
    /// <seealso cref="IHostedService" />
    public class EngineHostedService : IHostedService
    {
        public EngineHostedService(IEngine engine, IHubContext<NotificationHub> hubContext)
        {
            this.engine = engine;
            this.hubContext = hubContext;
        }
        private Timer timer;
        private readonly IEngine engine;
        private readonly IHubContext<NotificationHub> hubContext;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            engine.Init();
            engine.OnJobStarted += Engine_OnJobStarted;
            engine.OnTaskStarted += Engine_OnTaskStarted;
            timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(2));
            return Task.CompletedTask;
        }

        private void Engine_OnTaskStarted(Guid taskId)
        {
            hubContext.Clients.All.SendAsync("TaskStarted", taskId.ToString());
        }

        private void Engine_OnJobStarted(Guid jobId)
        {
            hubContext.Clients.All.SendAsync("JobStarted", jobId.ToString());
        }

        private void DoWork(object state)
        {
            hubContext.Clients.All.SendAsync("JobStarted", "prova");
            hubContext.Clients.All.SendAsync("TaskStarted", "prova");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            engine.OnJobStarted -= Engine_OnJobStarted;
            engine.OnTaskStarted -= Engine_OnTaskStarted;
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
