using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Models
{
    public interface IJobEntry
    {
        //DateTime? BeginTime { get; set; }
        DateTime? CreationDate { get; set; }
        DateTime? DeletionDate { get; set; }
        string Description { get; set; }
        //DateTime? EndTime { get; set; }
        bool FailIfAnyTaskHasError { get; set; }
        bool FailIfAnyTaskHasWarning { get; set; }
        Guid Id { get; set; }
        bool IsDeleted { get; set; }
        bool IsEnabled { get; set; }
        //bool IsPaused { get; set; }
        string Key { get; set; }
        DateTime? LastUpdateDate { get; set; }
        List<IInstanceEntry> Instances { get; set; }
        string Name { get; set; }
        List<ITaskEntry> Tasks { get; set; }
        List<ITriggerEntry> Triggers { get; set; }
    }
}
