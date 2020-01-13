using bs.Data.Interfaces;
using bS.Sked2.Model.UI;
using bS.Sked2.Service.Base;
using bS.Sked2.Shared;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.UI;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bS.Sked2.Service.UI
{
    public partial class EngineUIService : ServiceBase, IEngineUIService
    {
        private readonly IEngineRepository engineRepository;
        private readonly IEngineUIServiceConfig configuration;
        private readonly IUnitOfWork uow;

        public EngineUIService(
            ILogger logger,
            IUnitOfWork uow,
            IEngineRepository engineRepository,
            IEngineUIServiceConfig configuration) : base(logger)
        {
            this.uow = uow;
            this.engineRepository = engineRepository;
            this.configuration = configuration;
        }

        #region Elements

        public bool AddModuleToElement(Guid elementId, Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewElement(IElementDefinitionCreate elementDefinition)
        {
            throw new NotImplementedException();
        }

        public void DeleteElement(Guid elementId)
        {
            throw new NotImplementedException();
        }

        public void EditElement(Guid elementId, IElementDefinitionEdit elementDefinition)
        {
            throw new NotImplementedException();
        }

        public IElementDefinitionCreate GetCreateElement()
        {
            return new ElementDefinitionCreateViewModel
            {
                Name = "New Element",
                Description = "New Element description",
                ElementTypesList = GetElementTypes()
            };
        }

        private IEnumerable<IElementType> GetElementTypes()
        {
            return AssembliesExtensions.GetTypesImplementingInterface<IEngineElement>( new string[] { configuration.ExtensionsFolder }, true)
                      .Select(ed => new ElementTypeViewModel
                      {
                          Key = (string)ed.GetProperty("KeyConst")?.GetValue(ed),
                          Name = (string)ed.GetProperty("Name")?.GetValue(ed),
                          Description = (string)ed.GetProperty("Description")?.GetValue(ed)
                      });
        }

        public IEnumerable<IElementDefinitionDetail> GetElements(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IElementDefinitionPreview> GetElementsPreview(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModuleDefinitionDetail> GetModulesForElement(IElementType elementType)
        {
            throw new NotImplementedException();
        }

        public void MoveElementDown(Guid elementId)
        {
            throw new NotImplementedException();
        }

        public void MoveElementUp(Guid elementId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveModuleFromElement(Guid elementId, Guid moduleId)
        {
            throw new NotImplementedException();
        }

        #endregion Elements

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

        #region Tasks


        #endregion Tasks

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