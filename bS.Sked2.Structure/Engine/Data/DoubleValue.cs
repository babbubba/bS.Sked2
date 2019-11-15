using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class DoubleValue : BaseEngineValue
    {
        public DoubleValue()
        {
            CanPersistInStorage = true;
            dataType = DataType.Double;
        }

        public DoubleValue(double value)
        {
            CanPersistInStorage = true;
            dataType = DataType.Double;
            Set(value);
        }
    }
}
