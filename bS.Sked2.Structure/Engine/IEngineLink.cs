using bS.Sked2.Structure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Base interface for Links. A link is a flow component between two flow components that permits mapping between them.
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.IEngineFlowComponent" />
    public interface IEngineLink : IEngineFlowComponent
    {
        IElementEntry PreviuousElement { get; }
        IElementEntry NextElement { get; }
        IEnumerable<IElementsMappingEntry> Mappings { get; }

        ///// <summary>
        ///// Gets the data value.
        ///// </summary>
        ///// <param name="direction">The direction.</param>
        ///// <param name="propertyKey">The property key.</param>
        ///// <returns></returns>
        //IEngineData GetDataValue(EngineDataDirection direction, string propertyKey);

        ///// <summary>
        ///// Registers the input properties.
        ///// </summary>
        ///// <param name="key">The key.</param>
        ///// <param name="description">The description.</param>
        ///// <param name="dataType">Type of the data.</param>
        ///// <param name="mandatory">if set to <c>true</c> [mandatory].</param>
        //void RegisterInputProperties(string key, string description, DataType dataType, bool mandatory = false);

        ///// <summary>
        ///// Registers the output properties.
        ///// </summary>
        ///// <param name="key">The key.</param>
        ///// <param name="description">The description.</param>
        ///// <param name="dataType">Type of the data.</param>
        ///// <param name="mandatory">if set to <c>true</c> [mandatory].</param>
        //void RegisterOutputProperties(string key, string description, DataType dataType, bool mandatory = false);

        ///// <summary>
        ///// Sets the data value.
        ///// </summary>
        ///// <param name="direction">The direction.</param>
        ///// <param name="propertyKey">The property key.</param>
        ///// <param name="value">The value.</param>
        //void SetDataValue(EngineDataDirection direction, string propertyKey, IEngineData value);

        //void SetDataValueIfEmpty(EngineDataDirection direction, string propertyKey, IEngineData value);

    }
}
