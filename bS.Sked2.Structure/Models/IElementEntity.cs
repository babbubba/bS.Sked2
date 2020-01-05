using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Models
{
    public interface IElementEntity
    {
        DateTime? CreationDate { get; set; }
        DateTime? DeletionDate { get; set; }
        string Description { get; set; }
        bool IsDeleted { get; set; }
        bool IsEnabled { get; set; }
        string Key { get; set; }
        DateTime? LastUpdateDate { get; set; }
        IList<IInstanceEntry> Instances { get; set; }
        string Name { get; set; }
        ITaskEntry ParentTask { get; set; }
        IModuleEntry ParentModule { get; set; }
    }
}
