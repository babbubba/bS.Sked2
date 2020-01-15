using bS.Sked2.Structure.Engine.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model.UI
{
    public class ModuleDefinitionCreateViewModel : IModuleDefinitionCreate
    {
        public string ModuleTypeKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ParentTaskId { get; set; }
        public IEnumerable<IModuleType> ModuleTypesList { get; set; }
    }
}
