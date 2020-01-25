using System;

namespace bS.Sked2.Structure.Engine.UI
{
    public interface ITaskDefinitionEdit
    {
        Guid Id { get; set; }

        string Name { get; set; }
        string Description { get; set; }
        public bool FailIfAnyElementHasError { get; set; }
        public bool FailIfAnyElementHasWarning { get; set; }
        public bool IsEnabled { get; set; }


    }
}
