namespace bS.Sked2.Structure.Engine.UI
{
    public interface ITriggerDefinitionDetail
    {
        ITriggerType TriggerType { get; set; }

        string Name { get; set; }
        string Description { get; set; }
    }
}
