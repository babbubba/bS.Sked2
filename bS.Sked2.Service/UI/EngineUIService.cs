﻿using bs.Data.Interfaces;
using bS.Sked2.Service.Base;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.UI;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Service.UI
{
    public partial class EngineUIService : ServiceBase, IEngineUIService
    {
        private readonly IEngineRepository engineRepository;
        private readonly IEngineUIServiceConfig configuration;
        private readonly IUnitOfWork uow;
        private readonly IEngine engine;

        public EngineUIService(
            ILogger logger,
            IUnitOfWork uow,
            IEngine engine,
            IEngineRepository engineRepository,
            IEngineUIServiceConfig configuration) : base(logger)
        {
            this.uow = uow;
            this.engine = engine;
            this.engineRepository = engineRepository;
            this.configuration = configuration;
        }

      

        #region Links

        public Guid CreateNewLink(ILinkDefinitionCreate linkDefinition)
        {
            throw new NotImplementedException();
        }

        public void DeleteLink(Guid linkID)
        {
            throw new NotImplementedException();
        }

        public void EditLink(Guid linkID, ILinkDefinitionEdit linkDefinition)
        {
            throw new NotImplementedException();
        }

        public ILinkDefinitionCreate GetNewLink()
        {
            throw new NotImplementedException();
        }

        #endregion Links

        #region Modules

        public Guid CreateNewModule(IModuleDefinitionCreate moduleDefinition)
        {
            throw new NotImplementedException();
        }

        public void DeleteModule(Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public void EditModule(Guid moduleId, IModuleDefinitionEdit moduleDefinition)
        {
            throw new NotImplementedException();
        }

        public IModuleDefinitionCreate GetCreateModule()
        {
            throw new NotImplementedException();
        }

        #endregion Modules

        #region Triggers

        public Guid CreateNewTrigger(ITriggerDefinitionCreate triggerDefinition)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrigger(Guid triggerId)
        {
            throw new NotImplementedException();
        }

        public void EditTrigger(Guid triggerId, ITriggerDefinitionEdit triggerDefinition)
        {
            throw new NotImplementedException();
        }

        public ITriggerDefinitionCreate GetCreateTrigger()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITriggerDefinitionDetail> GetTriggers()
        {
            throw new NotImplementedException();
        }

        #endregion Triggers
    }
}