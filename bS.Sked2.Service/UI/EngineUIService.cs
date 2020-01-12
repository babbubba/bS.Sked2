using bs.Data.Interfaces;
using bS.Sked2.Model;
using bS.Sked2.Model.UI;
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
        private readonly IUnitOfWork uow;
        private readonly IEngineRepository engineRepository;

        public EngineUIService(
            ILogger logger,
            IUnitOfWork uow,
            IEngineRepository engineRepository) : base(logger)
        {
            this.uow = uow;
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

        public Guid CreateNewJob(IJobDefinitionCreate jobDefinition)
        {
            uow.BeginTransaction();

            var jobEntity = new JobEntry
            {
                Description = jobDefinition.Description,
                FailIfAnyTaskHasError = jobDefinition.FailIfAnyTaskHasError,
                FailIfAnyTaskHasWarning = jobDefinition.FailIfAnyTaskHasWarning,
                IsEnabled = true,
                Name = jobDefinition.Name,
                Position = engineRepository.GetJobs().Select(j=>j.Position).DefaultIfEmpty().Max() + 1
            };

            engineRepository.CreateJob(jobEntity);

            uow.Commit();

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

        /// <summary>
        /// Edits the job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="jobDefinition">The job definition.</param>
        public void EditJob(Guid jobId, IJobDefinitionEdit jobDefinition)
        {
            uow.BeginTransaction();
            var job = engineRepository.GetJobById(jobId);
            job.Description = jobDefinition.Description;
            job.FailIfAnyTaskHasError = jobDefinition.FailIfAnyTaskHasError;
            job.FailIfAnyTaskHasWarning = jobDefinition.FailIfAnyTaskHasWarning;
            job.IsEnabled = jobDefinition.IsEnabled;
            engineRepository.UpdateJob(job);
            uow.Commit();
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

        /// <summary>
        /// Gets the jobs.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IJobDefinitionDetail> GetJobs()
        {
            return engineRepository.GetJobs().Where(j=>!j.IsDeleted).OrderBy(j=>j.Position).Select(j => new JobDefinitionDetailViewModel
            {
                Id = j.Id,
                Description = j.Description,
                FailIfAnyTaskHasError = j.FailIfAnyTaskHasError,
                FailIfAnyTaskHasWarning = j.FailIfAnyTaskHasWarning,
                Name = j.Name,
                Position = j.Position,
                IsEnabled = j.IsEnabled,
                CreationDate = j.CreationDate,
                LastUpdateDate = j.LastUpdateDate
            });
        }

        public IEnumerable<ITriggerType> GetTriggerTypes()
        {
            throw new NotImplementedException();
        }
    }

  
}
