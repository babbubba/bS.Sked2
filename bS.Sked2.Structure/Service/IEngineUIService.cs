using bS.Sked2.Structure.Engine.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Service
{
    public interface IEngineUIService
    {
        IEnumerable<IElementType> GetElementTypes();
        IEnumerable<IModuleDefinition> GetExistingModulesForElementType(IElementType elementType);
        IEnumerable<ITriggerType> GetTriggerTypes();

        Guid CreateNewElement(IElementDefinition elementDefinition);
        void EditElement(Guid elementId, IElementDefinition elementDefinition);

        Guid CreateNewTask(ITaskDefinition taskDefinition);
        void EditTask(Guid taskId, IElementDefinition taskDefinition);

        Guid CreateNewJob(IJobDefinition jobDefinition);
        void EditJob(Guid jobId, IJobDefinition jobDefinition);

        Guid CreateNewModule(IModuleDefinition moduleDefinition);
        void EditModule(Guid moduleId, IModuleDefinition moduleDefinition);

        Guid CreateNewTrigger(ITriggerDefinition triggerDefinition);
        void EditTrigger(Guid triggerId, ITriggerDefinition triggerDefinition);

        bool AddTaskToJob(Guid JobId, Guid TaskId);
        bool AddTriggerToJob(Guid JobId, Guid TriggerId);
        bool AddElementToTask(Guid TaskId, Guid ElementId);
        bool AddModuleToElement(Guid ElementId, Guid ModuleId);

    }
}
