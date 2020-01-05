using bs.Data.Interfaces;
using bS.Sked2.Model;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;

namespace bS.Sked2.Service.Message
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork uow;
        private readonly IMessageRepository messageRepository;

        public MessageService(IUnitOfWork uow, IMessageRepository messageRepository)
        {
            this.uow = uow;
            this.messageRepository = messageRepository;
        }
        //public IMessageEntry CreateMessage(string message, MessageSeverity severity)
        //{
        //    var mess = new MessageEntry
        //    {
        //        Message = message,
        //        Severity = severity
        //    };
        //    return mess;
        //}

        public IMessageEntry CreateMessage(string message, IInstanceEntry instance, MessageSeverity severity)
        {
            var mess = new MessageEntry
            {
                Message = message,
                Severity = severity,
                Instance = instance
            };
            return mess;
        }

        public void SaveMessage(IMessageEntry message)
        {
            uow.BeginTransaction();
            messageRepository.CreateMessage(message);
            uow.Commit();
        }
    }
}
