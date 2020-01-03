using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    public class BoolValue : BaseEngineValue
    {
        public override DataType DataType => DataType.Bool;

        public override string StoragePrefixValue => "!*BOOL#";

        public override bool CanPersistInStorage => true;

        public BoolValue()
        {

        }
        public BoolValue(bool value)
        {
            Set(value);
        }

        public override void ReadFromStringValue(string stringValue)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(bool));
            StringReader sr = new StringReader(stringValue);

            using (XmlReader writer = XmlReader.Create(sr))
            {
                value = xmlSerializer.Deserialize(writer);
            }
        }
        //XmlSerializer xmlSerializer = new XmlSerializer(value.GetType());
        //StringReader sr = new StringReader(stringValue);

        //using (XmlReader writer = XmlReader.Create(sr))
        //{
        //   value = xmlSerializer.Deserialize(writer);
        //}

    }
}
