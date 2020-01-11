using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.UI
{
    public interface IJobDefinition
    {
        string Name { get; set; }
        string Description { get; set; }
        bool FailIfAnyTaskHasError { get; set; }
        bool FailIfAnyTaskHasWarning { get; set; }
    }
}
