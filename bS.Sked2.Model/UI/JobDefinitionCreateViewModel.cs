using bS.Sked2.Structure.Engine.UI;

namespace bS.Sked2.Model.UI
{
    public class JobDefinitionCreateViewModel : IJobDefinitionCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool FailIfAnyTaskHasError { get; set; }
        public bool FailIfAnyTaskHasWarning { get; set; }
    }
}
