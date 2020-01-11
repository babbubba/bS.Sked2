using bS.Sked2.Model;
using bS.Sked2.Service.Base;
using bS.Sked2.Structure.Repositories;
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

    public interface IEngineUIService
    {
        IEnumerable<IElementType> GetElementTypes();
        IEnumerable<IModuleDefinition> GetExistingModulesForElementType(IElementType elementType);
        IEnumerable<ITriggerType> GetTriggerTypes();

        Guid CreateNewElement(IElementDefinition elementDefinition);
        void EditElement(Guid elementId, IElementDefinition elementDefinition);

        Guid CreateNewTask(ITaskDefinition taskDefinition);
        void EditTask(Guid taskId, IElementDefinition taskDefinition);

        Guid CreateNewJob(IJobDefinition jobDefinition);
        void EditJob(Guid jobId, IJobDefinition jobDefinition);

        Guid CreateNewModule(IModuleDefinition moduleDefinition);
        void EditModule(Guid moduleId, IModuleDefinition moduleDefinition);

        Guid CreateNewTrigger(ITriggerDefinition triggerDefinition);
        void EditTrigger(Guid triggerId, ITriggerDefinition triggerDefinition);

        bool AddTaskToJob(Guid JobId, Guid TaskId);
        bool AddTriggerToJob(Guid JobId, Guid TriggerId);
        bool AddElementToTask(Guid TaskId, Guid ElementId);
        bool AddModuleToElement(Guid ElementId, Guid ModuleId);

    }

    public interface IElementType
    { 
    string Name { get; set; }
    string Description { get; set; }
    string Key { get; set; }
    }

    public interface IModuleType
    {
        string Name { get; set; }
        string Description { get; set; }
        string Key { get; set; }
    }

    public interface ITriggerType
    {
        string Name { get; set; }
        string Description { get; set; }
        string Key { get; set; }
    }

    public interface IElementDefinition
    {
        IElementType ElementType { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }

    public interface ITaskDefinition
    {
        string Name { get; set; }
        string Description { get; set; }
    }

    public interface IJobDefinition
    {
        string Name { get; set; }
        string Description { get; set; }
        bool FailIfAnyTaskHasError { get; set; }
        bool FailIfAnyTaskHasWarning { get; set; }

    }

    public interface IModuleDefinition
    {
        IModuleType ModuleType { get; set; }

        string Name { get; set; }
        string Description { get; set; }
    }

    public interface ITriggerDefinition
    {
        ITriggerType TriggerType { get; set; }

        string Name { get; set; }
        string Description { get; set; }
    }
}
