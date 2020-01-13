﻿using bs.Data.Interfaces;
using bS.Sked2.Model;
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
    public class EngineLink : EngineElement, IEngineLink
    {
        public EngineLink(IUnitOfWork uow, IEngineRepository enginRepo, ILogger logger, IMessageService messageService) : base(uow, enginRepo, logger, messageService)
        {
            
        }
        public override string Key => "ElementsLink";

        public static string KeyConst => "ElementsLink";


        public override void LoadFromEntity(Guid EntityId)
        {
            elementEntry = engineRepository.GetElementById(EntityId);
        }

        public IElementEntry PreviuousElement => ((ElementsLinkEntry)elementEntry).Previuous;

        public IElementEntry NextElement => ((ElementsLinkEntry)elementEntry).Next;

        public IEnumerable<IElementsMappingEntry> Mappings => ((ElementsLinkEntry) elementEntry).Mappings;

    }
}