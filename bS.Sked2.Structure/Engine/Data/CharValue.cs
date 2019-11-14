using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class CharValue : BaseEngineValue
    {
        public CharValue()
        {
            dataType = DataType.Char;
        }

        public CharValue(char value)
        {
            dataType = DataType.Char;
            Set(value);
        }
    }
}
