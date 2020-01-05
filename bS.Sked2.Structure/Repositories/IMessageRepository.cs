using bS.Sked2.Structure.Models;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Repositories
{
    public interface IMessageRepository
    {
        void CreateMessage(IMessageEntry message);

        IEnumerable<IMessageEntry> GetInstanceMessages(Guid instanceId);
    }
}