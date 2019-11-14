﻿using bS.Sked2.Structure.Engine;
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

        public EngineJob(ILogger logger, IMessageService messageService) : base(logger, messageService)
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

        public bool CanBeExecuted()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            isPaused = true;
            AddMessage("Job execution paused.");

        }

        public void Start()
        {
            // Create the instance ID for this element
            instanceId = new Guid();

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