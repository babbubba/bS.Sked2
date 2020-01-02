using bs.Data.Interfaces;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Engine.Objects
{
    public abstract class EngineModule : BaseEngineComponent, IEngineComponent, IEngineModule
    {
        protected readonly IUnitOfWork uow;
        protected readonly IEngineRepository engineRepository;
        protected IModuleEntry moduleEntry;


        public EngineModule(
            ILogger logger, 
            IMessageService messageService, 
            IUnitOfWork uow,
            IEngineRepository engineRepository) : base(logger, messageService)
        {
            this.uow = uow;
            this.engineRepository = engineRepository;
        }

        public abstract string Key { get; }

        public abstract void Init();
    }
}
