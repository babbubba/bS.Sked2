﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    public class DecimalValue : BaseEngineValue
    {
        public override DataType DataType => DataType.Decimal;
        public static DataType DataTypeConst => DataType.Decimal;


        public override bool CanPersistInStorage => true;
        public DecimalValue()
        {

        }
        public DecimalValue(decimal value)
        {
            Set(value);
        }

        public override void ReadFromStringValue(string stringValue)
        {
            DeserializeValueFromString<decimal>(stringValue);
        }
    }
}
