using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Service.UI
{
    public class EngineUIService : IEngineUIService
    {
        public bool AddElementToTask(Guid TaskId, Guid ElementId)
        {
            throw new NotImplementedException();
        }

        public bool AddModuleToElement(Guid ElementId, Guid ModuleId)
        {
            throw new NotImplementedException();
        }

        public bool AddTaskToJob(Guid JobId, Guid TaskId)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewElement(IElementDefinition elementDefinition)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewJob(IJobDefinition jobDefinition)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewModule(IModuleDefinition moduleDefinition)
        {
            throw new NotImplementedException();
        }

        public Guid CreateNewTask(ITaskDefinition taskDefinition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IElementType> GetAllElementTypes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModuleDefinition> GetExistingModulesForElementType(IElementType elementType)
        {
            throw new NotImplementedException();
        }
    }

    public interface IEngineUIService
    {
        IEnumerable<IElementType> GetAllElementTypes();
        IEnumerable<IModuleDefinition> GetExistingModulesForElementType(IElementType elementType);
        Guid CreateNewElement(IElementDefinition elementDefinition);
        Guid CreateNewTask(ITaskDefinition taskDefinition);
        Guid CreateNewJob(IJobDefinition jobDefinition);
        Guid CreateNewModule(IModuleDefinition moduleDefinition);

        bool AddTaskToJob(Guid JobId, Guid TaskId);
        bool AddElementToTask(Guid TaskId, Guid ElementId);
        bool AddModuleToElement(Guid ElementId, Guid ModuleId);

    }

    public interface IElementType
    { 
    string Name { get; set; }
    string Description { get; set; }
    string Key { get; set; }
    }

    public interface IModuleType
    {
        string Name { get; set; }
        string Description { get; set; }
        string Key { get; set; }
    }

    public interface IElementDefinition
    {
        IElementType ElementType { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }

    public interface ITaskDefinition
    {
        string Name { get; set; }
        string Description { get; set; }
    }

    public interface IJobDefinition
    {
        string Name { get; set; }
        string Description { get; set; }
    }

    public interface IModuleDefinition
    {
        IModuleDefinition ModuleType { get; set; }

        string Name { get; set; }
        string Description { get; set; }
    }
}
