using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Engine.Objects
{
    public class EngineJob : BaseEngineComponent, IEngineJob
    {
        protected DateTime? beginTime;
        protected DateTime? endTime;
        protected bool isPaused;

        public EngineJob(ILogger<EngineJob> logger, IMessageService messageService) : base(logger, messageService)
        {
        }

        public bool FailIfAnyTaskHasError { get; set; }
        public bool FailIfAnyTaskHasWarning { get; set; }

        public DateTime? BeginTime => beginTime;

        public DateTime? EndTime => endTime;

        public bool IsPaused => isPaused;

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning => beginTime != null && !isPaused && endTime == null;

        /// <summary>
        /// Gets a value indicating whether this instance has completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has completed; otherwise, <c>false</c>.
        /// </value>
        public bool HasCompleted => beginTime != null && !isPaused && endTime != null;

        public IEngineTask[] Tasks { get; set; }
        public IEngineTrigger[] Triggers { get; set; }
        public string Key { get ; set ; }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool CanBeExecuted()
        {
            throw new NotImplementedException();
        }

        public void LoadFromEntity(IJobEntry jobEntry)
        {
            this.beginTime = jobEntry.BeginTime;
            this.Description = jobEntry.Description;
            this.endTime = jobEntry.EndTime;
            this.FailIfAnyTaskHasError = jobEntry.FailIfAnyTaskHasError;
            this.FailIfAnyTaskHasWarning = jobEntry.FailIfAnyTaskHasWarning;
            this.instanceId = jobEntry.InstanceID;
            this.isPaused = jobEntry.IsPaused;
            this.Key = jobEntry.Key;
            this.Name = jobEntry.Name;
        }

        public void Pause()
        {
            isPaused = true;
            AddMessage("Job execution paused.");

        }

        public void Start()
        {
            // Create the instance ID for this element
            instanceId = Guid.NewGuid();

            // Set the execution begin time
            beginTime = DateTime.Now;

            // Add a message to notify the element started
            AddMessage("Job execution started.");
        }

        public void Stop()
        {
            // It set paused value to false in case this element was paused previously
            isPaused = false;

            // Set this element finish time
            endTime = DateTime.Now;

            // Add a message to notify the element finish execution
            AddMessage("Job execution finish.");
        }
    }
}
