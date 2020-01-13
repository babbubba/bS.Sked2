using bS.Sked2.Structure.Engine.UI;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Service
{
    /// <summary>
    ///
    /// </summary>
    public interface IEngineUIService
    {
        #region Elements

        /// <summary>
        /// Adds the module to element.
        /// </summary>
        /// <param name="elementId">The element identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns></returns>
        bool AddModuleToElement(Guid elementId, Guid moduleId);

        /// <summary>
        /// Creates the new element.
        /// </summary>
        /// <param name="elementDefinition">The element definition.</param>
        /// <returns></returns>
        Guid CreateNewElement(IElementDefinitionCreate elementDefinition);

        /// <summary>
        /// Deletes the element.
        /// </summary>
        /// <param name="elementId">The element identifier.</param>
        void DeleteElement(Guid elementId);

        /// <summary>
        /// Edits the element.
        /// </summary>
        /// <param name="elementId">The element identifier.</param>
        /// <param name="elementDefinition">The element definition.</param>
        void EditElement(Guid elementId, IElementDefinitionEdit elementDefinition);

        /// <summary>
        /// Gets the creation view model for the element.
        /// </summary>
        /// <returns></returns>
        IElementDefinitionCreate GetCreateElement();

        /// <summary>
        /// Gets the edit task.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <returns></returns>
        ITaskDefinitionEdit GetEditTask(Guid taskId);

        /// <summary>
        /// Gets the elements in the Task.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <returns></returns>
        IEnumerable<IElementDefinitionDetail> GetElements(Guid taskId);

        /// <summary>
        /// Gets the elements preview for the task. This is used to generate the graphic view of a task.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <returns></returns>
        IEnumerable<IElementDefinitionPreview> GetElementsPreview(Guid taskId);

        /// <summary>
        /// Moves the element down.
        /// </summary>
        /// <param name="elementId">The element identifier.</param>
        void MoveElementDown(Guid elementId);

        /// <summary>
        /// Moves the element up.
        /// </summary>
        /// <param name="elementId">The element identifier.</param>
        void MoveElementUp(Guid elementId);

        /// <summary>
        /// Removes the module from element.
        /// </summary>
        /// <param name="elementId">The element identifier.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns></returns>
        bool RemoveModuleFromElement(Guid elementId, Guid moduleId);

        #endregion Elements

        #region Links

        /// <summary>
        /// Creates the new link.
        /// </summary>
        /// <param name="linkDefinition">The link definition.</param>
        /// <returns></returns>
        Guid CreateNewLink(ILinkDefinitionCreate linkDefinition);

        /// <summary>
        /// Deletes the link.
        /// </summary>
        /// <param name="linkID">The link identifier.</param>
        void DeleteLink(Guid linkID);

        /// <summary>
        /// Edits the link.
        /// </summary>
        /// <param name="linkID">The link identifier.</param>
        /// <param name="linkDefinition">The link definition.</param>
        void EditLink(Guid linkID, ILinkDefinitionEdit linkDefinition);

        /// <summary>
        /// Gets the new link view model for creation.
        /// </summary>
        /// <returns></returns>
        ILinkDefinitionCreate GetNewLink();

        #endregion Links

        #region Tasks

        /// <summary>
        /// Adds the element to task.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="elementId">The element identifier.</param>
        /// <returns></returns>
        bool AddElementToTask(Guid taskId, Guid elementId);

        /// <summary>
        /// Clones the task and all related elements.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <returns></returns>
        Guid CloneTask(Guid taskId);

        /// <summary>
        /// Creates the new task.
        /// </summary>
        /// <param name="taskDefinition">The task definition.</param>
        /// <returns></returns>
        Guid CreateNewTask(ITaskDefinitionCreate taskDefinition);

        /// <summary>
        /// Edits the task.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="taskDefinition">The task definition.</param>
        void EditTask(Guid taskId, ITaskDefinitionEdit taskDefinition);

        /// <summary>
        /// Gets the create task.
        /// </summary>
        /// <returns></returns>
        ITaskDefinitionCreate GetCreateTask();

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ITaskDefinitionDetail> GetTasks();

        /// <summary>
        /// Moves the task down.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        void MoveTaskDown(Guid taskId);

        /// <summary>
        /// Moves the task up.
        /// </summary>
        /// <param name="taskid">The taskid.</param>
        void MoveTaskUp(Guid taskid);

        /// <summary>
        /// Removes the element from task.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="elementId">The element identifier.</param>
        /// <returns></returns>
        bool RemoveElementFromTask(Guid taskId, Guid elementId);

        #endregion Tasks

        #region Jobs

        /// <summary>
        /// Adds the task to job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="taskId">The task identifier.</param>
        /// <returns></returns>
        bool AddTaskToJob(Guid jobId, Guid taskId);

        /// <summary>
        /// Adds the trigger to job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="triggerId">The trigger identifier.</param>
        /// <returns></returns>
        bool AddTriggerToJob(Guid jobId, Guid triggerId);

        /// <summary>
        /// Creates the new job.
        /// </summary>
        /// <param name="jobDefinition">The job definition.</param>
        /// <returns></returns>
        Guid CreateNewJob(IJobDefinitionCreate jobDefinition);

        /// <summary>
        /// Deletes the job.
        /// </summary>
        /// <param name="jobID">The job identifier.</param>
        void DeleteJob(Guid jobID);

        /// <summary>
        /// Edits the job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="jobDefinition">The job definition.</param>
        void EditJob(Guid jobId, IJobDefinitionEdit jobDefinition);

        /// <summary>
        /// Gets the new job.
        /// </summary>
        /// <returns></returns>
        IJobDefinitionCreate GetCreateJob();

        /// <summary>
        /// Gets the edit job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        IJobDefinitionEdit GetEditJob(Guid jobId);

        /// <summary>
        /// Gets the jobs.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IJobDefinitionDetail> GetJobs();

        /// <summary>
        /// Moves the job down.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        void MoveJobDown(Guid jobId);

        /// <summary>
        /// Moves the job up.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        void MoveJobUp(Guid jobId);

        /// <summary>
        /// Removes the task from job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="taskId">The task identifier.</param>
        /// <returns></returns>
        bool RemoveTaskFromJob(Guid jobId, Guid taskId);

        /// <summary>
        /// Removes the trigger from job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="triggerId">The trigger identifier.</param>
        /// <returns></returns>
        bool RemoveTriggerFromJob(Guid jobId, Guid triggerId);

        #endregion Jobs

        #region Modules

        //IEnumerable<IModuleType> GetModuleTypes();
        /// <summary>
        /// Creates the new module.
        /// </summary>
        /// <param name="moduleDefinition">The module definition.</param>
        /// <returns></returns>
        Guid CreateNewModule(IModuleDefinitionCreate moduleDefinition);

        /// <summary>
        /// Deletes the module.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        void DeleteModule(Guid moduleId);

        /// <summary>
        /// Edits the module.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="moduleDefinition">The module definition.</param>
        void EditModule(Guid moduleId, IModuleDefinitionEdit moduleDefinition);

        /// <summary>
        /// Gets the modules for element.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        /// <returns></returns>
        IEnumerable<IModuleDefinitionDetail> GetModulesForElement(IElementType elementType);

        /// <summary>
        /// Gets the new module.
        /// </summary>
        /// <returns></returns>
        IModuleDefinitionCreate GetCreateModule();

        #endregion Modules

        #region Triggers

        /// <summary>
        /// Creates the new trigger.
        /// </summary>
        /// <param name="triggerDefinition">The trigger definition.</param>
        /// <returns></returns>
        Guid CreateNewTrigger(ITriggerDefinitionCreate triggerDefinition);

        /// <summary>
        /// Deletes the trigger.
        /// </summary>
        /// <param name="triggerId">The trigger identifier.</param>
        void DeleteTrigger(Guid triggerId);

        /// <summary>
        /// Edits the trigger.
        /// </summary>
        /// <param name="triggerId">The trigger identifier.</param>
        /// <param name="triggerDefinition">The trigger definition.</param>
        void EditTrigger(Guid triggerId, ITriggerDefinitionEdit triggerDefinition);

        /// <summary>
        /// Gets the new trigger.
        /// </summary>
        /// <returns></returns>
        ITriggerDefinitionCreate GetCreateTrigger();

        /// <summary>
        /// Gets the triggers.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ITriggerDefinitionDetail> GetTriggers();

        #endregion Triggers
    }
}