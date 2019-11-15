using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class BoolValue : BaseEngineValue
    {
        public BoolValue()
        {
            CanPersistInStorage = true;
            dataType = DataType.Bool;
        }

        public BoolValue(bool value)
        {
            CanPersistInStorage = true;
            dataType = DataType.Bool;
            Set(value);
        }
    }
}
