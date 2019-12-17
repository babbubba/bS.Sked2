using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Models
{
    public interface ITaskEntry
    {
        DateTime? BeginTime { get; }
        DateTime? CreationDate { get; set; }
        DateTime? DeletionDate { get; set; }
        string Description { get; set; }
        List<IElementEntity> Elements { get; set; }
        DateTime? EndTime { get; }
        bool FailIfAnyElementHasError { get; set; }
        bool FailIfAnyElementHasWarning { get; set; }
        bool HasCompleted { get; set; }
        Guid Id { get; set; }
        Guid? InstanceID { get; set; }
        bool IsDeleted { get; set; }
        bool IsEnabled { get; set; }
        bool IsPaused { get; set; }
        bool IsRunning { get; set; }
        string Key { get; set; }
        DateTime? LastUpdateDate { get; set; }
        List<IMessageEntry> Messages { get; set; }
        string Name { get; set; }
        IJobEntry ParentJob { get; set; }
    }
}
