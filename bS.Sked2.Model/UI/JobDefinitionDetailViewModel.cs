using bS.Sked2.Structure.Engine.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model.UI
{

    public class JobDefinitionDetailViewModel : IJobDefinitionDetail
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool FailIfAnyTaskHasError { get; set; }
        public bool FailIfAnyTaskHasWarning { get; set; }
        public int Position { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }

    
}
