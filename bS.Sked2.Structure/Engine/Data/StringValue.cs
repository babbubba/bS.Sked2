using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    public class StringValue : BaseEngineValue
    {
        public override DataType DataType => DataType.String;
        public static DataType DataTypeConst => DataType.String;


        public override bool CanPersistInStorage => true;
        public StringValue()
        {

        }
        public StringValue(string value)
        {
            Set(value);
        }

        public override void ReadFromStringValue(string stringValue)
        {
           DeserializeValueFromString<string>(stringValue);
        }

       
    }
}
