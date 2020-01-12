using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.UI
{
    public interface IJobDefinitionDetail
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool FailIfAnyTaskHasError { get; set; }
        bool FailIfAnyTaskHasWarning { get; set; }
        int Position { get; set; }
        bool IsEnabled { get; set; }
        DateTime? CreationDate { get; set; }
        DateTime? LastUpdateDate { get; set; }


    }
}
