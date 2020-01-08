using bs.Data.Interfaces;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Engine.Objects
{
    public abstract class EngineLink : EngineElement, IEngineLink
    {
        public EngineLink(IUnitOfWork uow, IEngineRepository enginRepo, ILogger<EngineElement> logger, IMessageService messageService) : base(uow, enginRepo, logger, messageService)
        {
        }

        public IElementEntry PreviuousElement => throw new NotImplementedException();

        public IElementEntry NextElement => throw new NotImplementedException();

    }
}
