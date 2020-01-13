using System;

namespace bS.Sked2.Structure.Engine.UI
{
    public interface ITaskDefinitionDetail
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool FailIfAnyElementHasError { get; set; }
        bool FailIfAnyElementHasWarning { get; set; }
        int Position { get; set; }
        bool IsEnabled { get; set; }
        DateTime? CreationDate { get; set; }
        DateTime? LastUpdateDate { get; set; }
    }
}
