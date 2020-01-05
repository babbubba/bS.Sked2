using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    public class DoubleValue : BaseEngineValue
    {
        public override DataType DataType => DataType.Double;
        public static DataType DataTypeConst => DataType.Double;


        public override bool CanPersistInStorage => true;
        public DoubleValue()
        {

        }
        public DoubleValue(double value)
        {
            Set(value);
        }

        public override void ReadFromStringValue(string stringValue)
        {
            DeserializeValueFromString<double>(stringValue);
        }
    }
}
