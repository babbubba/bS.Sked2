using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Interfaccia base per ogni dato in ingresso o in uscita da un Elemento <see cref="IEngineElement"/>
    /// </summary>
    public interface IEngineData
    {
        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public DataType DataType { get; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can persist in storage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can persist in storage; otherwise, <c>false</c>.
        /// </value>
        public bool CanPersistInStorage { get;  }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is filled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is filled; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilled { get;}
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; set; }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Set<T>(T value);
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        public T Get<T>();
        public Nullable<T> GetNullable<T>() where T : struct;

        /// <summary>
        /// Convert to string the value so it can persist on database.
        /// </summary>
        /// <returns></returns>
        public string WriteToStringValue();

        /// <summary>
        /// Reads from string the converted value so it can be read from database.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        public void ReadFromStringValue(string stringValue);

        //public void DeserializeValueFromString<T>(string stringValue);
    }

    public enum DataType
    { 
             @Int = 0,
             @Bool = 1,
             @Decimal = 2,
             @Double = 3,
             @Char = 4,
             @String = 5,
             @Datetime = 6,
             @Table = 7,
             @DictionaryEntry = 8,
             Collection = 9
    }

    public enum EngineDataDirection
    {
        Input = 0,
        Output = 1
    }
}
