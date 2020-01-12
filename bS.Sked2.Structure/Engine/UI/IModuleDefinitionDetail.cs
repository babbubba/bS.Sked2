namespace bS.Sked2.Structure.Engine.UI
{
    public interface IModuleDefinitionDetail
    {
        IModuleType ModuleType { get; set; }

        string Name { get; set; }
        string Description { get; set; }
    }
}
