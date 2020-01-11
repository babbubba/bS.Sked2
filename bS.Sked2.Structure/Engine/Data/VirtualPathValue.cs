using bS.Sked2.Structure.Base.FileSystem;
using bS.Sked2.Structure.Engine.Data.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine.Data
{
    /// <summary>
    /// The virtual path EngineData value
    /// </summary>
    /// <seealso cref="BaseEngineValue" />
    public class VirtualPathValue : BaseEngineValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualPathValue"/> class.
        /// </summary>
        public VirtualPathValue()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualPathValue"/> class.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public VirtualPathValue(VirtualPath value)
        {
            Set(value);
        }

        /// <summary>
        /// Gets the data type constant.
        /// </summary>
        /// <value>
        /// The data type constant.
        /// </value>
        public static DataType DataTypeConst => DataType.VirtualPath;

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
        public override DataType DataType => DataType.VirtualPath;

        /// <summary>
        /// Reads from string the converted value so it can be read from database.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        public override void ReadFromStringValue(string stringValue)
        {
            DeserializeValueFromString<VirtualPath>(stringValue);
        }
    }
}
