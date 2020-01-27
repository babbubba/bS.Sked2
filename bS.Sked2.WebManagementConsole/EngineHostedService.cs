using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Service.Messages;
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
        public EngineHostedService(IEngine engine, IHubContext<MessageNotificationHub> hubContext)
        {
            this.engine = engine;
            this.hubContext = hubContext;
        }
        private Timer timer;
        private readonly IEngine engine;
        private readonly IHubContext<MessageNotificationHub> hubContext;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            engine.Init();
            engine.JobStarted += Engine_OnJobStarted;
            engine.TaskStarted += Engine_OnTaskStarted;
            engine.ElementStarted += Engine_ElementStarted;
            engine.ModuleInited += Engine_ModuleInited;

            engine.JobFinished += Engine_JobFinished;
            engine.TaskFinished += Engine_TaskFinished;
            engine.ElementFinished += Engine_ElementFinished;

            timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void Engine_ElementFinished(Guid elementId, Guid instanceId)
        {
            hubContext.Clients.All.SendAsync("ElementFinished", elementId.ToString(), instanceId.ToString());
        }

        private void Engine_TaskFinished(Guid taskId, Guid instanceId)
        {
            hubContext.Clients.All.SendAsync("TaskFinished", taskId.ToString(), instanceId.ToString());
        }

        private void Engine_JobFinished(Guid jobId, Guid instanceId)
        {
            hubContext.Clients.All.SendAsync("JobFinished", jobId.ToString(), instanceId.ToString());
        }

        private void Engine_ModuleInited(Guid moduleId)
        {
            hubContext.Clients.All.SendAsync("ModuleInited", moduleId.ToString());
        }

        private void Engine_ElementStarted(Guid elementId, Guid instanceId)
        {
            hubContext.Clients.All.SendAsync("ElementStarted", elementId.ToString(), instanceId.ToString());
        }

        private void Engine_OnTaskStarted(Guid taskId, Guid instanceId)
        {
            hubContext.Clients.All.SendAsync("TaskStarted", taskId.ToString(), instanceId.ToString());
        }

        private void Engine_OnJobStarted(Guid jobId, Guid instanceId)
        {
            hubContext.Clients.All.SendAsync("JobStarted", jobId.ToString(), instanceId.ToString());
        }

        private void DoWork(object state)
        {
            //hubContext.Clients.All.SendAsync("SendMessage", "Messaggio di prova", MessageSeverity.Error);
            hubContext.Clients.All.SendAsync("DisplayNotify", "Messaggio di prova", MessageSeverity.Error);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            engine.JobStarted -= Engine_OnJobStarted;
            engine.TaskStarted -= Engine_OnTaskStarted;
            engine.ElementStarted -= Engine_ElementStarted;
            engine.ModuleInited -= Engine_ModuleInited;

            engine.JobFinished -= Engine_JobFinished;
            engine.TaskFinished -= Engine_TaskFinished;
            engine.ElementFinished -= Engine_ElementFinished;

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
