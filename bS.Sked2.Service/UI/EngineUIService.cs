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
            ILogger<EngineUIService> logger,
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
            IModuleEntry moduleEntry = null;
            using (var transaction = uow.BeginTransaction())
            {
                // Load parent task
                var taskEntry = engineRepository.GetTaskById(moduleDefinition.ParentTaskId);

                // Check if parent task exists
                if (taskEntry == null) throw new EngineException(logger, "Error creating new module. The parent task not exists.");

                // get the engine element
                IEngineModule engineModule = GetEngineModule(moduleDefinition.ModuleTypeKey);

                // Get the right entry model for this engine element type
                moduleEntry = (IModuleEntry)engineModule.GetEmptyEntity();

                // Populate the entry module whit data provided by user
                moduleEntry.Name = moduleDefinition.Name;
                moduleEntry.Description = moduleDefinition.Description;
                moduleEntry.IsEnabled = true;
                moduleEntry.ParentTask = taskEntry;

                // create the element in db
                engineRepository.CreateModule(moduleEntry);
            }
            return moduleEntry.Id;
        }

        public void DeleteModule(Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public void EditModule(Guid moduleId, IModuleDefinitionEdit moduleDefinition)
        {
            IModuleEntry entry = null;
            using (var transaction = uow.BeginTransaction())
            {
                entry = engineRepository.GetModuleById(moduleId);
                entry.Name = moduleDefinition.Name;
                entry.Description = moduleDefinition.Description;

                foreach (var inputProperty in moduleDefinition.InputProperties)
                {
                    var entryProperty = entry.InputProperties.FirstOrDefault(p => p.Key == inputProperty.Key);
                    var data = GetPopertyDataValue(entryProperty);
                    data.ReadFromStringValue(inputProperty.Value.Base64Decode());
                    entryProperty.Value = data.WriteToStringValue();
                }

                foreach (var outputProperties in moduleDefinition.OutputProperties)
                {
                    var entryProperty = entry.OutputProperties.FirstOrDefault(p => p.Key == outputProperties.Key);
                    var data = GetPopertyDataValue(entryProperty);
                    data.ReadFromStringValue(outputProperties.Value.Base64Decode());
                    entryProperty.Value = data.WriteToStringValue();
                }

                if (moduleDefinition.ParentTaskId != null && moduleDefinition.ParentTaskId != entry.ParentTask.Id)
                {
                    var parentTask = engineRepository.GetTaskById((Guid)moduleDefinition.ParentTaskId);
                    entry.ParentTask = parentTask;
                }

                engineRepository.UpdateModule(entry);
            }
        }

        public bool SetModuleTask(Guid moduleId, Guid parentTaskId)
        {
            try
            {
                using (var transaction = uow.BeginTransaction())
                {
                    var parentTask = engineRepository.GetTaskById(parentTaskId);
                    var module = engineRepository.GetModuleById(moduleId);
                    module.ParentTask = parentTask;
                    engineRepository.UpdateModule(module);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IModuleDefinitionCreate GetCreateModule()
        {
            return new ModuleDefinitionCreateViewModel
            {
                Name = "New Module",
                Description = "New Module description",
                ModuleTypesList = GetModuleTypes()
            };
        }

        public IModuleDefinitionEdit GetEditModule(Guid moduleId)
        {
            var result = new ModuleDefinitionEditViewModel();
            var entry = engineRepository.GetModuleById(moduleId);
            result.Name = entry.Name;
            result.Description = entry.Description;
            result.ParentTaskId = entry.ParentTask?.Id;

            // get the Engine module
            var engineModule = GetEngineModule(entry.Key);

            // get the properties value
            // input
            var inputProperties = new List<PropertyDefinition>();
            foreach (var inputProperty in entry.InputProperties)
            {
                var engineDataValue = GetPopertyDataValue(inputProperty);
                var propertyDetail = engineModule.InputProperties.FirstOrDefault(ip => ip.Key == inputProperty.Key);

                inputProperties.Add(new PropertyDefinition
                {
                    DataType = inputProperty.DataType,
                    Value = engineDataValue.WriteToStringValue().Base64Encode(),
                    Key = inputProperty.Key,
                    Description = propertyDetail.Description
                });
            }
            result.InputProperties = inputProperties;

            // output
            var outputProperties = new List<PropertyDefinition>();
            foreach (var outputProperty in entry.OutputProperties)
            {
                var engineDataValue = GetPopertyDataValue(outputProperty);
                var propertyDetail = engineModule.OutputProperties.FirstOrDefault(op => op.Key == outputProperty.Key);

                outputProperties.Add(new PropertyDefinition
                {
                    DataType = outputProperty.DataType,
                    Value = engineDataValue.WriteToStringValue().Base64Encode(),
                    Key = outputProperty.Key,
                    Description = propertyDetail.Description
                });
            }
            result.OutputProperties = outputProperties;

            return result;
        }

        public IEngineModule GetEngineModule(string moduleTypeKey)
        {
            var engineModuleType = AssembliesExtensions.GetTypesImplementingInterface<IEngineModule>(new string[] { configuration.ExtensionsFolder }, true)
                .FirstOrDefault(ed => (string)ed.GetProperty("KeyConst")?.GetValue(ed) == moduleTypeKey);

            // Check if the element type exists
            if (engineModuleType == null) throw new EngineException(logger, "Error. The module type not exists.");

            // Load the engine element using engine service provider
            var engineModule = (IEngineModule)engine.ServiceProvider.GetService(engineModuleType);
            return engineModule;
        }

        private IEnumerable<IModuleType> GetModuleTypes()
        {
            return AssembliesExtensions.GetTypesImplementingInterface<IEngineModule>(new string[] { configuration.ExtensionsFolder }, true)
                      .Select(ed => new ModuleTypeViewModel
                      {
                          Key = (string)ed.GetProperty("KeyConst")?.GetValue(ed),
                          Name = (string)ed.GetProperty("Name")?.GetValue(ed),
                          Description = (string)ed.GetProperty("Description")?.GetValue(ed)
                      });
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