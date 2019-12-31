using bS.Sked2.Structure.Base.Exceptions;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bS.Sked2.Engine
{
    public class Engine : IEngine
    {
        private readonly ILogger<Engine> logger;

        public Engine(ILogger<Engine> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Executes the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <exception cref="EngineException">
        /// The element '{element.Name}'  cannot be executed. This task was aborted.
        /// or
        /// The element {element.Name} (id: {element.InstanceID}) was not executed cause one or more errors occurs. This task was aborted.
        /// or
        /// The element {element.Name} (id: {element.InstanceID}) was executed but one or more warning occurs. This task was aborted.
        /// </exception>
        public void ExecuteElement(IEngineElement element)
        {
            // Check if element can be executed
            if (!element.CanBeExecuted())
            {
                element.AddMessage($"The element cannot be executed.", MessageSeverity.Error);
                if (element.ParentTask.FailIfAnyElementHasError)
                {
                    throw new EngineException(logger, $"The element cannot be executed. This task was aborted.");
                }
                else
                {
                    return;
                }
            }

            // Execute element
            element.Start();

            // stop element
            element.Stop();

            if (element.HasErrors)
            {
                element.AddMessage($"The element (instance: {element.InstanceID}) was not executed cause one or more errors occurs.", MessageSeverity.Error);
                if (element.ParentTask.FailIfAnyElementHasError)
                {
                    throw new EngineException(logger, $"The element (instance: {element.InstanceID}) was not executed cause one or more errors occurs. This task was aborted.");
                }
            }
            else if (element.HasWarnings)
            {
                element.AddMessage($"The element (instance: {element.InstanceID}) was executed but one or more warning occurs.", MessageSeverity.Warning);
                if (element.ParentTask.FailIfAnyElementHasWarning)
                {
                    throw new EngineException(logger, $"The element (instance: {element.InstanceID}) was executed but one or more warning occurs. This task was aborted.");
                }
            }
        }

        /// <summary>
        /// Executes the job.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ExecuteJob(IEngineJob job)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ExecuteTask(IEngineTask task)
        {
            // Check if task can be executed
            if (!task.CanBeExecuted())
            {
                task.AddMessage($"The task cannot be executed.", MessageSeverity.Error);
                if (task.ParentJob.FailIfAnyTaskHasError)
                {
                    throw new EngineException(logger, $"The task  cannot be executed. This job was aborted.");
                }
                else
                {
                    return;
                }
            }

            // Execute task
            task.Start();

            //Execute all active elements
            //foreach (var element in task.Elements)
            //{
            //    ExecuteElement(element);
            //}

            // Stop task
            task.Stop();
        }
    }
}
