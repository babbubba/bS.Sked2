using bS.Sked2.Structure.Base.Exceptions;
using System;
using System.Data;

namespace bS.Sked2.Structure.Engine.Data
{
    /// <summary>
    /// The base class for all IEngineData
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.IEngineData" />
    public abstract class BaseEngineValue : IEngineData
    {
        protected object value;
        protected DataType dataType;

        public DataType DataType => dataType;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can persist in storage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can persist in storage; otherwise, <c>false</c>.
        /// </value>
        public bool CanPersistInStorage => false;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is filled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is filled; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilled { get; set; }
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
        /// <returns></returns>
        public T Get<T>()
        {
            CheckType<T>();

            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Checks the type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="EngineException">
        /// Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'int').
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
        /// Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'DataTable').
        /// </exception>
        private void CheckType<T>()
        {
            switch (dataType)
            {
                case DataType.Int:
                    if (typeof(T) != typeof(int) || typeof(T) != typeof(int?))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'int').");
                    }
                    break;
                case DataType.Bool:
                    if (typeof(T) != typeof(bool) || typeof(T) != typeof(bool?))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'bool').");
                    }
                    break;
                case DataType.Decimal:
                    if (typeof(T) != typeof(decimal) || typeof(T) != typeof(decimal?))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'decimal').");
                    }
                    break;
                case DataType.Double:
                    if (typeof(T) != typeof(double) || typeof(T) != typeof(double?))
                    {
                        throw new EngineException("Error parsing Engine Data Value. The type of value is not the right type for this data (expected 'double').");
                    }
                    break;
                case DataType.Char:
                    if (typeof(T) != typeof(char) || typeof(T) != typeof(char?))
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
                    if (typeof(T) != typeof(DateTime) || typeof(T) != typeof(DateTime?))
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
            }
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Set<T>(T value)
        {
            CheckType<T>();
            this.value = value;
            IsValid = this.value != null;
            IsFilled = true;
        }


    }
}
