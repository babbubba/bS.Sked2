using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Models
{
    public interface IModuleEntry
    {
        DateTime? CreationDate { get; set; }
        DateTime? DeletionDate { get; set; }
        string Description { get; set; }
        bool IsDeleted { get; set; }
        bool IsEnabled { get; set; }
        string Key { get; set; }
        DateTime? LastUpdateDate { get; set; }
        List<IInstanceEntry> Instances { get; set; }
        string Name { get; set; }
    }
}
