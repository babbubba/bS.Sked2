using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    public class DateTimeValue : BaseEngineValue
    {
        public DateTimeValue()
        {
            CanPersistInStorage = true;
            dataType = DataType.Datetime;
        }

        public DateTimeValue(DateTime value)
        {
            CanPersistInStorage = true;
            dataType = DataType.Datetime;
            Set(value);
        }
    }
}
