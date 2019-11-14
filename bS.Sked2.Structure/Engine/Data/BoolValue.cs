using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class BoolValue : BaseEngineValue
    {
        public BoolValue()
        {
            dataType = DataType.Bool;
        }

        public BoolValue(bool value)
        {
            dataType = DataType.Bool;
            Set(value);
        }
    }
}
