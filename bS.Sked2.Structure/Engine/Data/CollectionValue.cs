using bS.Sked2.Shared;
using bS.Sked2.Structure.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    public class CollectionValue : BaseEngineValue
    {
        public override DataType DataType => DataType.Collection;

        public new bool IsFilled => (value != null && Count() > 0);

        public override bool CanPersistInStorage => true;
    
        public CollectionValue()
        {
            value = new List<IEngineData>();
        }

        public CollectionValue(IList<IEngineData> val)
        {
            value = new List<IEngineData>();
            Set(val);
        }

        public void AddValue(IEngineData val)
        {
            ((List<IEngineData>)value).Add(val);
        }

        public void RemoveValue(IEngineData val)
        {
            ((List<IEngineData>)value).Remove(val);
        }

        public int Count()
        {
            return ((List<IEngineData>)value).Count;
        }

        public override string WriteToStringValue()
        {
            if (Count() == 0) return "EMPTY";

            var xmlDoc = new XmlDocument();
            var rootNode = xmlDoc.CreateElement("Collection");
            rootNode.SetAttribute("Count", ((List<IEngineData>)value).Count.ToString());
            xmlDoc.AppendChild(rootNode);
        
            var idx = 0;

            foreach (var element in (List<IEngineData>)value)
            {
                var xmlElement = xmlDoc.CreateElement("Element");
                xmlElement.SetAttribute("DataType", element.DataType.ToString());
                xmlElement.InnerXml = element.WriteToStringValue();
                rootNode.AppendChild(xmlElement);
                idx++;
            }

            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true, 
                IndentChars = "  ",
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                xmlDoc.Save(writer);
            }
            return sb.ToString();
        }

        public override void ReadFromStringValue(string stringValue)
        {
            if (stringValue == "EMPTY")
            {
                value = new List<IEngineData>();
                return;
            }
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(stringValue);

            value = new List<IEngineData>();

            XmlNodeList elements = xmlDoc.SelectNodes("//Collection/Element");
            foreach (XmlNode element in elements)
            {
                Enum.TryParse(element.Attributes["DataType"].Value, out DataType dataType);
                IEngineData newEntry;
                switch (dataType)
                {
                    case DataType.Int:
                        newEntry = new IntValue();
                        break;
                    case DataType.Bool:
                        newEntry = new BoolValue();
                        break;
                    case DataType.Decimal:
                        newEntry = new DecimalValue();
                        break;
                    case DataType.Double:
                        newEntry = new DoubleValue();
                        break;
                    case DataType.Char:
                        newEntry = new CharValue();
                        break;
                    case DataType.String:
                        newEntry = new StringValue();
                        break;
                    case DataType.Datetime:
                        newEntry = new DateTimeValue();
                        break;
                    case DataType.Table:
                        newEntry = new TableValue();
                        break;
                    case DataType.DictionaryEntry:
                        newEntry = new DictionaryEntryValue();
                        break;
                    case DataType.Collection:
                        newEntry = new CollectionValue();
                        break;
                    default:
                        throw new EngineException("Invalid DataType. Cannot read this value type.");
                }

                newEntry.ReadFromStringValue(element.InnerXml);
                AddValue(newEntry);
            }
        }

    }
}
