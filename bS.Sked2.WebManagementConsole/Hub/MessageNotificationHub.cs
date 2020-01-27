using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.AspNetCore.SignalR;

namespace bS.Sked2.WebManagementConsole.Hub
{
    public class MessageNotificationHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task DisplayMessage(string message, MessageSeverity severity)
        {
            await Clients.All.SendAsync("DisplayMessage", message, severity);
        }

        public async Task DisplayNotify(string message, MessageSeverity severity)
        {
            await Clients.All.SendAsync("DisplayNotify", message, severity);
        }
    }
}
