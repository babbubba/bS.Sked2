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
        public EngineHostedService(
            IEngine engine, 
            IHubContext<MessageNotificationHub> messageNotificationHubContext,
            IHubContext<EngineNotificationHub> engineNotificationHubContext)
        {
            this.engine = engine;
            this.messageNotificationHubContext = messageNotificationHubContext;
            this.engineNotificationHubContext = engineNotificationHubContext;
        }
        private Timer timer;
        private readonly IEngine engine;
        private readonly IHubContext<MessageNotificationHub> messageNotificationHubContext;
        private readonly IHubContext<EngineNotificationHub> engineNotificationHubContext;

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

            messageNotificationHubContext.Clients.All.SendAsync(
               "DisplayNotify",
               "Engine service started",
               "The Engine Service was inited and started.",
               MessageSeverity.Info);

            return Task.CompletedTask;
        }

        private void Engine_ElementFinished(Guid elementId, Guid instanceId, bool success)
        {
            engineNotificationHubContext.Clients.All.SendAsync("ElementFinished", elementId.ToString(), instanceId.ToString(), success);
            
            MessageSeverity severity;
            string message;
            
            if (success)
            {
                message = $"Element {instanceId.ToString()} finished execution succesfully";
                severity = MessageSeverity.Success;
            }
            else
            {
                message = $"Element {instanceId.ToString()} failed execution.";
                severity = MessageSeverity.Error;
            }

            messageNotificationHubContext.Clients.All.SendAsync(
                "DisplayNotify", 
                $"Element finish",
                message,
                severity);

        }

        private void Engine_TaskFinished(Guid taskId, Guid instanceId, bool success)
        {
            engineNotificationHubContext.Clients.All.SendAsync("TaskFinished", taskId.ToString(), instanceId.ToString(), success);

            MessageSeverity severity;
            string message;

            if (success)
            {
                message = $"Task {instanceId.ToString()} finished execution succesfully";
                severity = MessageSeverity.Success;
            }
            else
            {
                message = $"Task {instanceId.ToString()} failed execution.";
                severity = MessageSeverity.Error;
            }

            messageNotificationHubContext.Clients.All.SendAsync(
                "DisplayNotify",
                $"Task finish",
                message,
                severity);
        }

        private void Engine_JobFinished(Guid jobId, Guid instanceId, bool success)
        {
            engineNotificationHubContext.Clients.All.SendAsync("JobFinished", jobId.ToString(), instanceId.ToString(), success);

            MessageSeverity severity;
            string message;

            if (success)
            {
                message = $"Job {instanceId.ToString()} finished execution succesfully";
                severity = MessageSeverity.Success;
            }
            else
            {
                message = $"Job {instanceId.ToString()} failed execution.";
                severity = MessageSeverity.Error;
            }

            messageNotificationHubContext.Clients.All.SendAsync(
                "DisplayNotify",
                $"Job finish",
                message,
                severity);
        }

        private void Engine_ModuleInited(Guid moduleId, bool success)
        {
            engineNotificationHubContext.Clients.All.SendAsync("ModuleInited", moduleId.ToString(), success);
        }

        private void Engine_ElementStarted(Guid elementId, Guid instanceId)
        {
            engineNotificationHubContext.Clients.All.SendAsync("ElementStarted", elementId.ToString(), instanceId.ToString());

            messageNotificationHubContext.Clients.All.SendAsync(
             "DisplayNotify",
             $"Element start",
             $"Element {instanceId.ToString()} start",
             MessageSeverity.Info);
        }

        private void Engine_OnTaskStarted(Guid taskId, Guid instanceId)
        {
            engineNotificationHubContext.Clients.All.SendAsync("TaskStarted", taskId.ToString(), instanceId.ToString());

            messageNotificationHubContext.Clients.All.SendAsync(
            "DisplayNotify",
            $"Task start",
            $"Task {instanceId.ToString()} start",
            MessageSeverity.Info);
        }

        private void Engine_OnJobStarted(Guid jobId, Guid instanceId)
        {
            engineNotificationHubContext.Clients.All.SendAsync("JobStarted", jobId.ToString(), instanceId.ToString());

            messageNotificationHubContext.Clients.All.SendAsync(
            "DisplayNotify",
            $"Job start",
            $"Job {instanceId.ToString()} start",
            MessageSeverity.Info);
        }

        private void DoWork(object state)
        {
            //Random random = new Random();
            //int number = random.Next(0, 4) * 10;
            //messageNotificationHubContext.Clients.All.SendAsync("DisplayNotify", "Titolo di prova", "Messaggio di prova", number);
           
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
