using bs.Data.Interfaces;
using bS.Sked2.Model.Repositories;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bS.Sked2.Engine.Objects
{
    public class EngineJob : BaseEngineComponent, IEngineJob
    {
        private readonly IUnitOfWork uow;
        private readonly IEngineRepository engineRepository;
        private IJobEntry jobEntry;
        public EngineJob(
            IUnitOfWork uow,
            IEngineRepository enginRepo,
            ILogger<EngineJob> logger, 
            IMessageService messageService) : base(logger, messageService)
        {
            this.uow = uow;
            this.engineRepository = enginRepo;
        }


        /// <summary>
        /// Determines whether this instance [can be executed].
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance [can be executed]; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanBeExecuted()
        {
            // TODO: Add logic here if needed
            return true;
        }

        /// <summary>
        /// Loads from entity.
        /// </summary>
        /// <param name="JobId">The job identifier.</param>
        public override void LoadFromEntity(Guid JobId)
        {
            jobEntry = engineRepository.GetJobById(JobId);
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public override void Pause()
        {
            uow.BeginTransaction();

            instance.IsPaused = true;

            AddMessage("Job execution paused.");

            uow.Commit();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public override void Start()
        {
            uow.BeginTransaction();

            // Create the instance ID for this element
            instance = engineRepository.CreateNewInstance();

            // Set the execution begin time
            instance.BeginTime = DateTime.Now;

            // Add a message to notify the element started
            AddMessage("Job execution started.");

            uow.Commit();

            //TODO: Execute Job Logic
            
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            uow.BeginTransaction();

            // It set paused value to false in case this element was paused previously
            instance.IsPaused = false;

            // Set this element finish time
            instance.EndTime = DateTime.Now;

            // Add a message to notify the element finish execution
            AddMessage("Job execution finish.");

            uow.Commit();
        }
    }
}
