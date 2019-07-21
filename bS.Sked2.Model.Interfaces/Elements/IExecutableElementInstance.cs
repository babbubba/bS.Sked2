using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Model.Interfaces.Elements
{
    public interface IExecutableElementInstance
    {
        DateTime BeginDate { get; set; }
        DateTime EndDate { get; set; }
        bool HasErrors { get; set; }
        bool HasWarning { get; set; }
        bool Success { get; set; }
    }
}
