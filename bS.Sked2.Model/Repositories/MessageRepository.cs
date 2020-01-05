using bs.Data;
using bs.Data.Interfaces;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Model.Repositories
{
    public class MessageRepository : Repository, IMessageRepository
    {
        public MessageRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateMessage(IMessageEntry message)
        {
            base.Create((MessageEntry)message);
        }

        public IEnumerable<IMessageEntry> GetInstanceMessages(Guid instanceId)
        {
            var instanceEntry = base.GetById<InstanceEntry>(instanceId);
            return instanceEntry.Messages;
        }
    }
}