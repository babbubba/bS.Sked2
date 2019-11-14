using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
   public  class IntValue : BaseEngineValue
    {
        public IntValue()
        {
            dataType = DataType.Int;
        }

        public IntValue(int value)
        {
            dataType = DataType.Int;
            Set(value);
        }
    }
}
