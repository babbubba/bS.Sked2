using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
   public  class IntValue : BaseEngineValue
    {
        public override DataType DataType => DataType.Int;

        public override bool CanPersistInStorage => true;
        public IntValue()
        {

        }
        public IntValue(int value)
        {
            Set(value);
        }

        public override void ReadFromStringValue(string stringValue)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(int));
            StringReader sr = new StringReader(stringValue);

            using (XmlReader writer = XmlReader.Create(sr))
            {
                value = xmlSerializer.Deserialize(writer);
            }
        }

    }
}
