﻿using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Engine.UI
{
    public interface IModuleDefinitionEdit
    {
        string Name { get; set; }
        string Description { get; set; }
        Guid? ParentTaskId { get; set; }
        IEnumerable<IPropertyDefinition> InputProperties { get; set; }
        IEnumerable<IPropertyDefinition> OutputProperties { get; set; }
    }
}
