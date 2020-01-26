using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace bS.Sked2.WebManagementConsole.Hub
{
    public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task JobStart(string jobId)
        {
            await Clients.All.SendAsync("JobStarted", jobId);
        }

        public async Task TaskStart(string taskId)
        {
            await Clients.All.SendAsync("TaskStarted", taskId);
        }
    }
}
