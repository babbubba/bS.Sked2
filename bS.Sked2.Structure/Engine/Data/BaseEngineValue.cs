using bS.Sked2.Structure.Base.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace bS.Sked2.Structure.Engine.Data
{
    /// <summary>
    /// The base class for all IEngineData
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.IEngineData" />
    public abstract class BaseEngineValue : IEngineData
    {
        /// <summary>
        /// The value
        /// </summary>
        protected object value;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can persist in storage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can persist in storage; otherwise, <c>false</c>.
        /// </value>
        public abstract bool CanPersistInStorage { get; }

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public abstract DataType DataType { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is filled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is filled; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilled => this.value != null;

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            CheckType<T>();
            if (value == null) return default;
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Gets the nullable value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Nullable<T> GetNullable<T>() where T : struct
        {
            CheckType<T>();
            if (value == null) return null;
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Reads from string the converted value so it can be read from database.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        public abstract void ReadFromStringValue(string stringValue);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        public void Set<T>(T value)
        {
            CheckType<T>();
            this.value = value;
        }

        /// <summary>
        /// Convert to string the value so it can persist on database.
        /// </summary>
        /// <returns></returns>
        public virtual string WriteToStringValue()
        {
            if (value == null) return "NULL";
            XmlSerializer xmlSerializer = new XmlSerializer(value.GetType());
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
                xmlSerializer.Serialize(writer, value);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Deserializes the value from string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringValue">The string value.</param>
        internal void DeserializeValueFromString<T>(string stringValue)
        {
            if (stringValue == "NULL") return;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringReader sr = new StringReader(stringValue);
            using (XmlReader writer = XmlReader.Create(sr))
            {
                value = xmlSerializer.Deserialize(writer);
            }
        }

        /// <summary>
        /// Checks the type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="EngineException">Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'int').
        /// or
        /// Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'bool').
        /// or
        /// Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'decimal').
        /// or
        /// Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'double').
        /// or
        /// Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'char').
        /// or
        /// Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'string').
        /// or
        /// Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'DateTime').
        /// or
        /// Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'DataTable').</exception>
        private void CheckType<T>()
        {
            switch (DataType)
            {
                case DataType.Int:
                    if (typeof(T) != typeof(int) && typeof(T) != typeof(int?))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'int').");
                    }
                    break;

                case DataType.Bool:
                    if (typeof(T) != typeof(bool) && typeof(T) != typeof(bool?))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'bool').");
                    }
                    break;

                case DataType.Decimal:
                    if (typeof(T) != typeof(decimal) && typeof(T) != typeof(decimal?))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'decimal').");
                    }
                    break;

                case DataType.Double:
                    if (typeof(T) != typeof(double) && typeof(T) != typeof(double?))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'double').");
                    }
                    break;

                case DataType.Char:
                    if (typeof(T) != typeof(char) && typeof(T) != typeof(char?))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'char').");
                    }
                    break;

                case DataType.String:
                    if (typeof(T) != typeof(string))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'string').");
                    }
                    break;

                case DataType.Datetime:
                    if (typeof(T) != typeof(DateTime) && typeof(T) != typeof(DateTime?))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'DateTime').");
                    }
                    break;

                case DataType.Table:
                    if (typeof(T) != typeof(DataTable))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'DataTable').");
                    }
                    break;

                case DataType.DictionaryEntry:
                    if (typeof(T) != typeof(DictionaryEntry))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'DictionaryEntry').");
                    }
                    break;

                case DataType.Collection:
                    if (typeof(T) != typeof(List<IEngineData>))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'List<IEngineData>').");
                    }
                    break;
            }
        }
    }
}