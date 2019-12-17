using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Models
{
    public interface IElementEntity
    {
        DateTime? CreationDate { get; set; }
        DateTime? DeletionDate { get; set; }
        Guid Id { get; set; }
        bool IsDeleted { get; set; }
        bool IsEnabled { get; set; }
        DateTime? LastUpdateDate { get; set; }
    }
}
