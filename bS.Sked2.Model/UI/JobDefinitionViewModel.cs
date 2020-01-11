using bS.Sked2.Structure.Engine.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model.UI
{
    public class JobDefinitionViewModel : IJobDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool FailIfAnyTaskHasError { get; set; }
        public bool FailIfAnyTaskHasWarning { get; set; }
    }
}
