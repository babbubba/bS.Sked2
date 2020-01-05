using bs.Data.Interfaces;
using bS.Sked2.Model.Repositories;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Engine.Objects
{
    public class EngineTask : BaseEngineComponent, IEngineTask
    {
        private readonly IUnitOfWork uow;
        private readonly IEngineRepository engineRepository;
        private ITaskEntry taskEntry;

        public EngineTask(
            IUnitOfWork uow,
            IEngineRepository engineRepository,
            ILogger<EngineTask> logger,
            IMessageService messageService) : base(logger, messageService)
        {
            this.uow = uow;
            this.engineRepository = engineRepository;
        }

        public IJobEntry ParentJob { get => taskEntry.ParentJob; }

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
        /// <param name="taskId">The job identifier.</param>
        public override void LoadFromEntity(Guid taskId)
        {
            taskEntry = engineRepository.GetTaskById(taskId);
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public override void Pause()
        {
            uow.BeginTransaction();

            instance.IsPaused = true;

            AddMessage("Task execution paused.");

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

            // Add current instance to entry
            taskEntry.Instances.Add(instance);

            // Add a message to notify the element started
            AddMessage("Task execution started.");

            uow.Commit();

            //TODO: Execute Task Logic

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
