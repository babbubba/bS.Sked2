﻿using System;
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

        public string StoragePrefixValue { get; }

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

        public string WriteToStringValue();
        public void ReadFromStringValue(string stringValue);
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
