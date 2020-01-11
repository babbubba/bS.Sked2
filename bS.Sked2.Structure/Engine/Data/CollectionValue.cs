using bS.Sked2.Structure.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace bS.Sked2.Structure.Engine.Data
{
    /// <summary>
    /// The collection EngineData value
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.Data.BaseEngineValue" />
    public class CollectionValue : BaseEngineValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionValue"/> class.
        /// </summary>
        public CollectionValue()
        {
            value = new List<IEngineData>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionValue"/> class.
        /// </summary>
        /// <param name="val">The value.</param>
        public CollectionValue(IList<IEngineData> val)
        {
            value = new List<IEngineData>();
            Set(val);
        }

        /// <summary>
        /// Gets the data type constant.
        /// </summary>
        /// <value>
        /// The data type constant.
        /// </value>
        public static DataType DataTypeConst => DataType.Collection;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can persist in storage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can persist in storage; otherwise, <c>false</c>.
        /// </value>
        public override bool CanPersistInStorage => true;

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public override DataType DataType => DataType.Collection;

        /// <summary>
        /// Gets a value indicating whether this instance is filled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is filled; otherwise, <c>false</c>.
        /// </value>
        public new bool IsFilled => (value != null && Count() > 0);

        /// <summary>
        /// Adds the value.
        /// </summary>
        /// <param name="val">The value.</param>
        public void AddValue(IEngineData val)
        {
            ((List<IEngineData>)value).Add(val);
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return ((List<IEngineData>)value).Count;
        }

        /// <summary>
        /// Reads from string the converted value so it can be read from database.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <exception cref="bS.Sked2.Structure.Base.Exceptions.EngineException">Invalid DataType. Cannot read this value type.</exception>
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
                    case DataType.VirtualPath:
                        newEntry = new VirtualPathValue();
                        break;

                    default:
                        throw new EngineException("Invalid DataType. Cannot read this value type.");
                }

                newEntry.ReadFromStringValue(element.InnerXml);
                AddValue(newEntry);
            }
        }

        /// <summary>
        /// Removes the value.
        /// </summary>
        /// <param name="val">The value.</param>
        public void RemoveValue(IEngineData val)
        {
            ((List<IEngineData>)value).Remove(val);
        }

        /// <summary>
        /// Convert to string the value so it can persist on database.
        /// </summary>
        /// <returns></returns>
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
    }
}