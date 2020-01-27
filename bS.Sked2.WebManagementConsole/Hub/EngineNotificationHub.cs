using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bS.Sked2.WebManagementConsole.Hub
{
    public class EngineNotificationHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task JobStarted(string jobId, string instanceId)
        {
            await Clients.All.SendAsync("JobStarted", jobId, instanceId);
        }

        public async Task TaskStarted(string taskId, string instanceId)
        {
            await Clients.All.SendAsync("TaskStarted", taskId, instanceId);
        }

        public async Task Jobfinished(string jobId, string instanceId)
        {
            await Clients.All.SendAsync("Jobfinished", jobId, instanceId);
        }

        public async Task Taskfinished(string taskId, string instanceId)
        {
            await Clients.All.SendAsync("Taskfinished", taskId, instanceId);
        }
    }
}
