using bS.Sked2.Model;
using bS.Sked2.Model.UI;
using bS.Sked2.Service.Base;
using bS.Sked2.Structure.Engine.UI;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bS.Sked2.Service.UI
{
    public partial class EngineUIService : ServiceBase, IEngineUIService
    {
        /// <summary>
        /// Adds the task to job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="taskId">The task identifier.</param>
        /// <returns></returns>
        public bool AddTaskToJob(Guid jobId, Guid taskId)
        {
            try
            {
                using (var transaction = uow.BeginTransaction())
                {
                    var jobEntry = engineRepository.GetJobById(jobId);
                    var taskEntry = engineRepository.GetTaskById(taskId);
                    jobEntry.Tasks.Add(taskEntry);
                    taskEntry.ParentJob = jobEntry;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error adding task {taskId.ToString()} to job {jobId.ToString()}.", ex);
                return false;
            }

            return true;
        }

        public bool AddTriggerToJob(Guid jobId, Guid triggerId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the new job.
        /// </summary>
        /// <param name="jobDefinition">The job definition.</param>
        /// <returns></returns>
        public Guid CreateNewJob(IJobDefinitionCreate jobDefinition)
        {
            var jobEntity = new JobEntry
            {
                Description = jobDefinition.Description,
                FailIfAnyTaskHasError = jobDefinition.FailIfAnyTaskHasError,
                FailIfAnyTaskHasWarning = jobDefinition.FailIfAnyTaskHasWarning,
                IsEnabled = true,
                Name = jobDefinition.Name,
                Position = engineRepository.GetJobs().Select(j => j.Position).DefaultIfEmpty().Max() + 1
            };

            using (var transaction = uow.BeginTransaction())
            {
                engineRepository.CreateJob(jobEntity);
            }

            return jobEntity.Id;
        }

        /// <summary>
        /// Deletes the job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        public void DeleteJob(Guid jobId)
        {
            using (var transaction = uow.BeginTransaction())
            {
                var entry = engineRepository.GetJobById(jobId);
                entry.IsDeleted = true;
                foreach (var task in entry.Tasks)
                {
                    task.IsDeleted = true;
                    foreach (var element in task.Elements)
                    {
                        element.IsDeleted = true;
                    }
                }
            }
        }

        /// <summary>
        /// Edits the job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="jobDefinition">The job definition.</param>
        public void EditJob(Guid jobId, IJobDefinitionEdit jobDefinition)
        {
            using (var transaction = uow.BeginTransaction())
            {
                var job = engineRepository.GetJobById(jobId);
                job.Name = jobDefinition.Name;
                job.Description = jobDefinition.Description;
                job.FailIfAnyTaskHasError = jobDefinition.FailIfAnyTaskHasError;
                job.FailIfAnyTaskHasWarning = jobDefinition.FailIfAnyTaskHasWarning;
                job.IsEnabled = jobDefinition.IsEnabled;
                engineRepository.UpdateJob(job);
            }
        }

        /// <summary>
        /// Gets the new job.
        /// </summary>
        /// <returns></returns>
        public IJobDefinitionCreate GetCreateJob()
        {
            return new JobDefinitionCreateViewModel
            {
                Name = "New Job",
                Description = "New Job description"
            };
        }

        /// <summary>
        /// Gets the edit job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        public IJobDefinitionEdit GetEditJob(Guid jobId)
        {
            var entry = engineRepository.GetJobById(jobId);
            return new JobDefinitionEditViewModel
            {
                Name = entry.Name,
                Description = entry.Description,
                FailIfAnyTaskHasError = entry.FailIfAnyTaskHasError,
                FailIfAnyTaskHasWarning = entry.FailIfAnyTaskHasWarning,
                IsEnabled = entry.IsEnabled
            };
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

        public void MoveJobDown(Guid jobId)
        {
            throw new NotImplementedException();
        }

        public void MoveJobUp(Guid jobId)
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
    }
}