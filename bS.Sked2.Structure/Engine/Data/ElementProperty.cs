namespace bS.Sked2.Structure.Engine.Data
{
    /// <summary>
    /// Property containing value attached to element 
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.IEngineElementProperty" />
    public class ElementProperty : IEngineElementProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementProperty"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="mandatory">if set to <c>true</c> [mandatory].</param>
        public ElementProperty(string key, string description, DataType dataType, bool mandatory)
        {
            Key = key;
            Description = description;
            DataType = dataType;
            Mandatory = mandatory;
        }

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public DataType DataType { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ElementProperty"/> is mandatory.
        /// </summary>
        /// <value>
        ///   <c>true</c> if mandatory; otherwise, <c>false</c>.
        /// </value>
        public bool Mandatory { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public IEngineData Value { get; set; }
    }
}