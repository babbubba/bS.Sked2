using System;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Interface for all EngineData />
    /// </summary>
    public interface IEngineData
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance can persist in storage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can persist in storage; otherwise, <c>false</c>.
        /// </value>
        public bool CanPersistInStorage { get; }

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public DataType DataType { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is filled; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilled { get; }

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
        public T Get<T>();

        /// <summary>
        /// Gets the nullable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Nullable<T> GetNullable<T>() where T : struct;

        /// <summary>
        /// Reads from string the converted value so it can be read from database.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        public void ReadFromStringValue(string stringValue);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        public void Set<T>(T value);

        /// <summary>
        /// Convert to string the value so it can persist on database.
        /// </summary>
        /// <returns></returns>
        public string WriteToStringValue();

        //public void DeserializeValueFromString<T>(string stringValue);
    }
}