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

        #endregion Jobs
    }
}