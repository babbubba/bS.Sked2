using System;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// The engine interface
    /// </summary>
    public interface IEngine
    {
        public IEngineConfig Configuration { get; }

        /// <summary>
        /// Executes the element.
        /// </summary>
        /// <param name="element">The element.</param>
        void ExecuteElement(IEngineElement element);

        /// <summary>
        /// Executes the job.
        /// </summary>
        /// <param name="job">The job.</param>
        void ExecuteJob(IEngineJob job);

        /// <summary>
        /// Executes the job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        void ExecuteJob(Guid jobId);

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="task">The task.</param>
        void ExecuteTask(IEngineTask task);

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Init();

        IServiceProvider ServiceProvider { get; }
    }
}