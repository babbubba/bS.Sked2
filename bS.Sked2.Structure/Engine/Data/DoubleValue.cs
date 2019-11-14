using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class DoubleValue : BaseEngineValue
    {
        public DoubleValue()
        {
            dataType = DataType.Double;
        }

        public DoubleValue(double value)
        {
            dataType = DataType.Double;
            Set(value);
        }
    }
}
