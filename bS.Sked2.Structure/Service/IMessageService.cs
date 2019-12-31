using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Service.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Service
{

    public interface IMessageService
    {
        /// <summary>
        /// Creates the generic message instance.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="severity">The severity.</param>
        /// <returns></returns>
        IMessageEntry CreateMessage(string Message, MessageSeverity severity);
       
        IMessageEntry CreateMessage(string message, IInstanceEntry instance, MessageSeverity severity);

    }
}
