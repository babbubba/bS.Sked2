namespace bS.Sked2.Structure.Engine.UI
{
    public interface ITaskDefinitionCreate
    {
        string Name { get; set; }
        string Description { get; set; }

        bool FailIfAnyElementHasError { get; set; }
        bool FailIfAnyElementHasWarning { get; set; }
    }
}
