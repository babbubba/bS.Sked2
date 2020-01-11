namespace bS.Sked2.Structure.Engine.Data
{
    /// <summary>
    /// The string EngineData value
    /// </summary>
    /// <seealso cref="BaseEngineValue" />
    public class StringValue : BaseEngineValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringValue"/> class.
        /// </summary>
        public StringValue()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringValue"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public StringValue(string value)
        {
            Set(value);
        }

        /// <summary>
        /// Gets the data type constant.
        /// </summary>
        /// <value>
        /// The data type constant.
        /// </value>
        public static DataType DataTypeConst => DataType.String;

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
        public override DataType DataType => DataType.String;

        /// <summary>
        /// Reads from string the converted value so it can be read from database.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        public override void ReadFromStringValue(string stringValue)
        {
            DeserializeValueFromString<string>(stringValue);
        }
    }
}