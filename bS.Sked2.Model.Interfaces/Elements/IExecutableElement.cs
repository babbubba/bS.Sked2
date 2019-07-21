using System;
using System.Collections.Generic;
using bS.Sked2.DtoModel.Interfaces;
using bS.Sked2.Model.Interfaces.Elements;

namespace bS.Sked2.Model.Interfaces.Elements
{
    public interface IExecutableElement
    {
        DateTime CreateDate { get; set; }
        string Description { get; set; }
        int Height { get; set; }
        Guid Id { get; set; }
        IEnumerable<IInterchangeablyBaseDto> InParameters { get; set; }
        IEnumerable<IExecutableElementInstance> Instances { get; set; }
        bool IsEnable { get; set; }
        string Name { get; set; }
        IEnumerable<IInterchangeablyBaseDto> OutParameters { get; set; }
        bool StopParentIfErrorOccurs { get; set; }
        bool StopParentIfWarningOccurs { get; set; }
        DateTime UpdateDate { get; set; }
        int Width { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}