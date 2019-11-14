using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    class DecimalValue : BaseEngineValue
    {
        public DecimalValue()
        {
            dataType = DataType.Decimal;
        }

        public DecimalValue(decimal value)
        {
            dataType = DataType.Decimal;
            Set(value);
        }
    }
}
