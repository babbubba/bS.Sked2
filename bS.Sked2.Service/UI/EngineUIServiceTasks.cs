using bS.Sked2.Model;
using bS.Sked2.Model.UI;
using bS.Sked2.Service.Base;
using bS.Sked2.Structure.Engine.UI;
using bS.Sked2.Structure.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bS.Sked2.Service.UI
{
    public partial class EngineUIService : ServiceBase, IEngineUIService
    {
        public bool AddElementToTask(Guid taskId, Guid elementId)
        {
            throw new NotImplementedException();
        }

        public Guid CloneTask(Guid taskId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the new task.
        /// </summary>
        /// <param name="taskDefinition">The task definition.</param>
        /// <returns></returns>
        public Guid CreateNewTask(ITaskDefinitionCreate taskDefinition)
        {
            var taskEntity = new TaskEntry
            {
                Description = taskDefinition.Description,
                FailIfAnyElementHasError = taskDefinition.FailIfAnyElementHasError,
                FailIfAnyElementHasWarning = taskDefinition.FailIfAnyElementHasWarning,
                IsEnabled = true,
                Name = taskDefinition.Name,
                Position = engineRepository.GetTasks().Select(t => t.Position).DefaultIfEmpty().Max() + 1
            };

            using (var transaction = uow.BeginTransaction())
            {
                engineRepository.CreateTask(taskEntity);
            }

            return taskEntity.Id;
        }

        public void EditTask(Guid taskId, ITaskDefinitionEdit taskDefinition)
        {
            using (var transaction = uow.BeginTransaction())
            {
                var entry = engineRepository.GetTaskById(taskId);
                entry.Name = taskDefinition.Name;
                entry.Description = taskDefinition.Description;
                entry.FailIfAnyElementHasError = taskDefinition.FailIfAnyElementHasError;
                entry.FailIfAnyElementHasWarning = taskDefinition.FailIfAnyElementHasWarning;
                entry.IsEnabled = taskDefinition.IsEnabled;
            }
        }

        /// <summary>
        /// Gets the create task.
        /// </summary>
        /// <returns></returns>
        public ITaskDefinitionCreate GetCreateTask()
        {
            return new TaskDefinitionCreateViewModel
            {
                Name = "New Task",
                Description = "New Task description"
            };
        }

        /// <summary>
        /// Gets the edit task.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <returns></returns>
        public ITaskDefinitionEdit GetEditTask(Guid taskId)
        {
            var entry = engineRepository.GetTaskById(taskId);
            return new TaskDefinitionEditViewModel
            {
                Name = entry.Name,
                Description = entry.Description,
                FailIfAnyElementHasError = entry.FailIfAnyElementHasError,
                FailIfAnyElementHasWarning = entry.FailIfAnyElementHasWarning,
                IsEnabled = entry.IsEnabled
            };
        }

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ITaskDefinitionDetail> GetTasks()
        {
            return engineRepository.GetTasks()
               .Where(t => !t.IsDeleted && t.IsEnabled)
               .OrderBy(t => t.Position)
               .Select(t => new TaskDefinitionDetailViewModel
               {
                   Id = t.Id,
                   Description = t.Description,
                   FailIfAnyElementHasError = t.FailIfAnyElementHasError,
                   FailIfAnyElementHasWarning = t.FailIfAnyElementHasWarning,
                   Name = t.Name,
                   Position = t.Position,
                   IsEnabled = t.IsEnabled,
                   CreationDate = t.CreationDate,
                   LastUpdateDate = t.LastUpdateDate
               });
        }

        public void MoveTaskDown(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public void MoveTaskUp(Guid taskid)
        {
            throw new NotImplementedException();
        }

        public bool RemoveElementFromTask(Guid taskId, Guid elementId)
        {
            throw new NotImplementedException();
        }
    }
}