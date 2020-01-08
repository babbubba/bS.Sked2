using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Models
{
    public interface ITaskEntry
    {
        DateTime? CreationDate { get; set; }

        DateTime? DeletionDate { get; set; }
        string Description { get; set; }
        IList<IElementEntry> Elements { get; set; }

        bool FailIfAnyElementHasError { get; set; }

        bool FailIfAnyElementHasWarning { get; set; }

        Guid Id { get; set; }

        IList<IInstanceEntry> Instances { get; set; }
        bool IsDeleted { get; set; }
        bool IsEnabled { get; set; }

        string Key { get; set; }

        DateTime? LastUpdateDate { get; set; }
        string Name { get; set; }
        IJobEntry ParentJob { get; set; }
    }
}