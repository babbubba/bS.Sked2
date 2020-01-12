namespace bS.Sked2.Structure.Engine.UI
{
    public interface IJobDefinitionEdit
    {
        string Description { get; set; }
        bool FailIfAnyTaskHasError { get; set; }
        bool FailIfAnyTaskHasWarning { get; set; }
        bool IsEnabled { get; set; }
    }
}
