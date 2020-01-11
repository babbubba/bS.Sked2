using bS.Sked2.Model;
using bS.Sked2.Service.Base;
using bS.Sked2.Structure.Engine.UI;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bS.Sked2.Service.UI
{
    public class EngineUIService : ServiceBase, IEngineUIService
    {
        private readonly IEngineRepository engineRepository;

        public EngineUIService(
            ILogger logger,
            IEngineRepository engineRepository) : base(logger)
        {
            this.engineRepository = engineRepository;
        }

        public bool AddElementToTask(Guid TaskId, Guid ElementId)
        {
            throw new NotImplementedException();
        }

        public bool AddModuleToElement(Guid ElementId, Guid ModuleId)
        {
            throw new NotImplementedException();
        }

        public bool AddTaskToJob(Guid JobId, Guid TaskId)
        {
            throw new NotImplementedException();
        }

        public bool AddTriggerToJob(Guid JobId, Guid TriggerId)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewElement(IElementDefinition elementDefinition)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewJob(IJobDefinition jobDefinition)
        {
            var jobEntity = new JobEntry
            {
                Description = jobDefinition.Description,
                FailIfAnyTaskHasError = jobDefinition.FailIfAnyTaskHasError,
                FailIfAnyTaskHasWarning = jobDefinition.FailIfAnyTaskHasWarning,
                IsEnabled = true,
                Name = jobDefinition.Name,
                Position = engineRepository.GetJobs()?.Max(j => j.Position) + 1 ?? 0
            };

            engineRepository.CreateJob(jobEntity);

            return jobEntity.Id;
        }

        public Guid CreateNewModule(IModuleDefinition moduleDefinition)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewTask(ITaskDefinition taskDefinition)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewTrigger(ITriggerDefinition triggerDefinition)
        {
            throw new NotImplementedException();
        }

        public void EditElement(Guid elementId, IElementDefinition elementDefinition)
        {
            throw new NotImplementedException();
        }

        public void EditJob(Guid jobId, IJobDefinition jobDefinition)
        {
            throw new NotImplementedException();
        }

        public void EditModule(Guid moduleId, IModuleDefinition moduleDefinition)
        {
            throw new NotImplementedException();
        }

        public void EditTask(Guid taskId, IElementDefinition taskDefinition)
        {
            throw new NotImplementedException();
        }

        public void EditTrigger(Guid triggerId, ITriggerDefinition triggerDefinition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IElementType> GetElementTypes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModuleDefinition> GetExistingModulesForElementType(IElementType elementType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITriggerType> GetTriggerTypes()
        {
            throw new NotImplementedException();
        }
    }

  
}
