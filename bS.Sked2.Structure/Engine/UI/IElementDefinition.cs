﻿namespace bS.Sked2.Structure.Engine.UI
{
    public interface IElementDefinition
    {
        IElementType ElementType { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
