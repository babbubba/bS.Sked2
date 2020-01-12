﻿using bs.Data.Interfaces;
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

namespace bS.Sked2.Service.UI
{
    public class EngineUIService : ServiceBase, IEngineUIService
    {
        private readonly IEngineRepository engineRepository;
        private readonly IUnitOfWork uow;

        public EngineUIService(
            ILogger logger,
            IUnitOfWork uow,
            IEngineRepository engineRepository) : base(logger)
        {
            this.uow = uow;
            this.engineRepository = engineRepository;
        }

        public bool AddElementToTask(Guid taskId, Guid elementId)
        {
            throw new NotImplementedException();
        }

        public bool AddModuleToElement(Guid elementId, Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public bool AddTaskToJob(Guid jobId, Guid taskId)
        {
            throw new NotImplementedException();
        }

        public bool AddTriggerToJob(Guid jobId, Guid triggerId)
        {
            throw new NotImplementedException();
        }

        public Guid CloneTask(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewElement(IElementDefinitionCreate elementDefinition)
        {
            throw new NotImplementedException();
        }

        #region Jobs

        /// <summary>
        /// Creates the new job.
        /// </summary>
        /// <param name="jobDefinition">The job definition.</param>
        /// <returns></returns>
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
                Position = engineRepository.GetJobs().Select(j => j.Position).DefaultIfEmpty().Max() + 1
            };

            engineRepository.CreateJob(jobEntity);

            uow.Commit();

            return jobEntity.Id;
        }

        public Guid CreateNewLink(ILinkDefinitionCreate linkDefinition)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewModule(IModuleDefinitionCreate moduleDefinition)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewTask(ITaskDefinitionCreate taskDefinition)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewTrigger(ITriggerDefinitionCreate triggerDefinition)
        {
            throw new NotImplementedException();
        }

        public void DeleteElement(Guid elementId)
        {
            throw new NotImplementedException();
        }

        public void DeleteLink(Guid linkID)
        {
            throw new NotImplementedException();
        }

        public void DeleteModule(Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrigger(Guid triggerId)
        {
            throw new NotImplementedException();
        }

        public void EditElement(Guid elementId, IElementDefinitionEdit elementDefinition)
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

        public void EditLink(Guid linkID, ILinkDefinitionEdit linkDefinition)
        {
            throw new NotImplementedException();
        }

        public void EditModule(Guid moduleId, IModuleDefinitionEdit moduleDefinition)
        {
            throw new NotImplementedException();
        }

        public void EditTask(Guid taskId, IElementDefinitionEdit taskDefinition)
        {
            throw new NotImplementedException();
        }

        public void EditTrigger(Guid triggerId, ITriggerDefinitionEdit triggerDefinition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IElementDefinitionDetail> GetElements(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IElementDefinitionPreview> GetElementsPreview(Guid taskId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the active jobs ordered by position ascending.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IJobDefinitionDetail> GetJobs()
        {
            return engineRepository.GetJobs()
                .Where(j => !j.IsDeleted && j.IsEnabled)
                .OrderBy(j => j.Position)
                .Select(j => new JobDefinitionDetailViewModel
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

        public IEnumerable<IModuleDefinitionDetail> GetModulesForElement(IElementType elementType)
        {
            throw new NotImplementedException();
        }

        public IElementDefinitionCreate GetNewElement()
        {
            throw new NotImplementedException();
        }

        public IJobDefinitionCreate GetNewJob()
        {
            return new JobDefinitionCreateViewModel
            {
                Name = "New Job",
                Description = "New Job description"
            };
        }

        public ILinkDefinitionCreate GetNewLink()
        {
            throw new NotImplementedException();
        }

        public IModuleDefinitionCreate GetNewModule()
        {
            throw new NotImplementedException();
        }

        public ITaskDefinitionCreate GetNewTask()
        {
            throw new NotImplementedException();
        }

        public ITriggerDefinitionCreate GetNewTrigger()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITaskDefinitionDetail> GetTasks(Guid jobId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITriggerDefinitionDetail> GetTriggers()
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

        public void MoveJobDown(Guid jobId)
        {
            throw new NotImplementedException();
        }

        public void MoveJobUp(Guid jobId)
        {
            throw new NotImplementedException();
        }

        public void MoveTaskDown(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public void MoveTaskUp(Guid taskid)
        {
            throw new NotImplementedException();
        }

        public bool RemoveElementFromTask(Guid taskId, Guid elementId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveModuleFromElement(Guid elementId, Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTaskFromJob(Guid jobId, Guid taskId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTriggerFromJob(Guid jobId, Guid triggerId)
        {
            throw new NotImplementedException();
        }

        #endregion Jobs
    }
}