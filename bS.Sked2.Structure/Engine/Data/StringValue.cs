using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class StringValue : BaseEngineValue
    {
        public StringValue()
        {
            dataType = DataType.String;
        }

        public StringValue(string value)
        {
            dataType = DataType.String;
            Set(value);
        }
    }
}
