using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
   public  class IntValue : BaseEngineValue
    {
        public IntValue()
        {
            CanPersistInStorage = true;
            dataType = DataType.Int;
        }

        public IntValue(int value)
        {
            CanPersistInStorage = true;
            dataType = DataType.Int;
            Set(value);
        }
    }
}
