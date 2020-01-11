using System.Collections;

namespace bS.Sked2.Structure.Engine.Data
{
    /// <summary>
    /// The dictionary entry EngineData value
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.Data.BaseEngineValue" />
    public class DictionaryEntryValue : BaseEngineValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryEntryValue"/> class.
        /// </summary>
        public DictionaryEntryValue()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryEntryValue"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public DictionaryEntryValue(string key, string value)
        {
            var entry = new DictionaryEntry(key, value);
            Set(entry);
        }

        /// <summary>
        /// Gets the data type constant.
        /// </summary>
        /// <value>
        /// The data type constant.
        /// </value>
        public static DataType DataTypeConst => DataType.DictionaryEntry;

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
        public override DataType DataType => DataType.DictionaryEntry;

        //public override string WriteToStringValue()
        //{
        //    var sb = new StringBuilder();
        //    sb.Append(StoragePrefixValue);
        //    sb.Append(((DictionaryEntry)value).Key);
        //    sb.Append("|!§@#");
        //    sb.Append(((DictionaryEntry)value).Value?.ToString()??"");
        //    return sb.ToString();
        //}

        /// <summary>
        /// Reads from string the converted value so it can be read from database.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        public override void ReadFromStringValue(string stringValue)
        {
            DeserializeValueFromString<DictionaryEntry>(stringValue);
        }
    }
}