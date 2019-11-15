using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class CharValue : BaseEngineValue
    {
        public CharValue()
        {
            CanPersistInStorage = true;
            dataType = DataType.Char;
        }

        public CharValue(char value)
        {
            CanPersistInStorage = true;
            dataType = DataType.Char;
            Set(value);
        }
    }
}
