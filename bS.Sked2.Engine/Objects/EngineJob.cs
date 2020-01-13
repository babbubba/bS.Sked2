using bs.Data.Interfaces;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Engine.Objects
{
    public class EngineJob : BaseEngineComponent, IEngineJob
    {
        private readonly IEngineRepository engineRepository;
        private readonly IServiceProvider serviceProvider;
        private readonly IEngine engine;
        private readonly IUnitOfWork uow;
        private IJobEntry jobEntry;

        public EngineJob(
            IUnitOfWork uow,
            IEngineRepository enginRepo,
            ILogger logger,
            IMessageService messageService,
            IServiceProvider serviceProvider,
            IEngine engine) : base(logger, messageService)
        {
            this.uow = uow;
            this.engineRepository = enginRepo;
            this.serviceProvider = serviceProvider;
            this.engine = engine;
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
            using (var transaction = uow.BeginTransaction())
            {
                instance.IsPaused = true;
            }

            AddMessage("Job execution paused.");

        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public override void Start()
        {
            using (var transaction = uow.BeginTransaction())
            {

                // Create the instance ID for this element
                instance = engineRepository.CreateNewInstance();

                // Set the execution begin time
                instance.BeginTime = DateTime.Now;

                // Add current instance to entry
                jobEntry.Instances.Add(instance);

            }

            // Add a message to notify the element started
            AddMessage("Job execution started.");

            // Creating the engine tasks flow
            var engineTasksFlow = new List<IEngineTask>();
            foreach (var taskEntry in jobEntry.Tasks)
            {
                var engineTask = serviceProvider.GetService<IEngineTask>();
                engineTask.LoadFromEntity(taskEntry.Id);
                engineTasksFlow.Add(engineTask);
            }

            foreach (var engineTask in engineTasksFlow)
            {
                engine.ExecuteTask(engineTask);
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            using (var transaction = uow.BeginTransaction())
            {

                // It set paused value to false in case this element was paused previously
                instance.IsPaused = false;

                // Set this element finish time
                instance.EndTime = DateTime.Now;

            }

            // Add a message to notify the element finish execution
            AddMessage("Job execution finish.");
        }
    }
}