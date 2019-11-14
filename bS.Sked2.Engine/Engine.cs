using bS.Sked2.Structure.Base.Exceptions;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        public void ExecuteElement(IEngineElement element)
        {
            // Check if element can be executed
            if (!element.CanBeExecuted())
            {
                //if (element.ParentTask.FailIfAnyElementHasError)
                //{
                element.ParentTask.AddMessage($"The element {element.Name} (id: {element.InstanceID}) cannot be executed. The task will be aborted.", MessageSeverity.Fatal);
                //element.ParentTask.Stop();
                //}
                throw new EngineException(logger, $"The element {element.Name} (id: {element.InstanceID}) cannot be executed.");
            }

            // Execute element
            element.Start();

            element.Stop();
        }

        public void ExecuteJob(IEngineJob job)
        {
            throw new NotImplementedException();
        }

        public void ExecuteTask(IEngineTask task)
        {
            throw new NotImplementedException();
        }
    }
}
