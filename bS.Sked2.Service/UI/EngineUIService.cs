using bs.Data.Interfaces;
using bS.Sked2.Model.UI;
using bS.Sked2.Service.Base;
using bS.Sked2.Shared;
using bS.Sked2.Structure.Base.Exceptions;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.UI;
using bS.Sked2.Structure.Models;
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

        #region Elements

        public bool AddModuleToElement(Guid elementId, Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewElement(IElementDefinitionCreate elementDefinition)
        {
            IElementEntry elementEntry = null;
            using (var transaction = uow.BeginTransaction())
            {
                // Load parent task
                var taskEntry = engineRepository.GetTaskById(elementDefinition.ParentTaskId);

                // Check if parent task exists
                if (taskEntry == null) throw new EngineException(logger, "Error creating new element. The parent task not exists.");

                // Search the engine element model type
                var engineElementType = AssembliesExtensions.GetTypesImplementingInterface<IEngineElement>(new string[] { configuration.ExtensionsFolder }, true)
                    .FirstOrDefault(ed => (string)ed.GetProperty("KeyConst")?.GetValue(ed) == elementDefinition.ElementTypeKey);

                // Check if the element type exists
                if (engineElementType == null) throw new EngineException(logger, "Error creating new element. The element type not exists.");

                //// Init the engine if needed
                //if (engine.ServiceProvider == null) engine.Init();

                // Load the engine element using engine service provider
                var engineElement = (IEngineElement)engine.ServiceProvider.GetService(engineElementType);

                // Get the right entry model for this engine element type
                elementEntry = engineElement.GetEmptyEntity();

                // Populate the entry element whit data provided by user
               // elementEntry.ParentTask = taskEntry;
                elementEntry.Name = elementDefinition.Name;
                elementEntry.Description = elementDefinition.Description;
                elementEntry.IsEnabled = true;

                // create the element in db
                engineRepository.CreateElement(elementEntry);
            }

            // Add new element to parent task
            AddElementToTask(elementDefinition.ParentTaskId, elementEntry.Id);

            return elementEntry.Id;

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