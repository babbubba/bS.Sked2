using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    public interface IEngine
    {
        ILogger<IEngine> Logger { get; }
        /// <summary>
        /// Executes the job.
        /// </summary>
        /// <param name="job">The job.</param>
        void ExecuteJob(IEngineJob job);
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="task">The task.</param>
        void ExecuteTask(IEngineTask task);
        /// <summary>
        /// Executes the element.
        /// </summary>
        /// <param name="element">The element.</param>
        void ExecuteElement(IEngineElement element);
    }
}
