namespace bS.Sked2.Structure.Engine.UI
{
    public interface IElementPropertyDefinition
    {
        DataType DataType { get; set; }
        string Value { get; set; }
        string Key { get; set; }
        string Description { get; set; }
    }
}
