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

        /// <summary>
        /// Gets the service provider for internal dependency injecton.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Delegate job's event
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="instanceId">The instance identifier.</param>
        delegate void JobStartableEvent(Guid jobId, Guid instanceId);

        /// <summary>
        ///  Delegate task's event
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="instanceId">The instance identifier.</param>
        delegate void TaskStartableEbent(Guid taskId, Guid instanceId);

        /// <summary>
        ///  Delegate element's event
        /// </summary>
        /// <param name="elementId">The element identifier.</param>
        /// <param name="instanceId">The instance identifier.</param>
        delegate void ElementStartableEvent(Guid elementId, Guid instanceId);

        /// <summary>
        /// Delegate module's event
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        delegate void ModuleStartableEvent(Guid moduleId);

        event JobStartableEvent JobStarted;
        event JobStartableEvent JobFinished;

        event TaskStartableEbent TaskStarted;
        event TaskStartableEbent TaskFinished;

        event ElementStartableEvent ElementStarted;
        event ElementStartableEvent ElementFinished;

        event ModuleStartableEvent ModuleInited;
    }
}