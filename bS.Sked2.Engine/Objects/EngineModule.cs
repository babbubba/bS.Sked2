using bs.Data.Interfaces;
using bS.Sked2.Shared;
using bS.Sked2.Structure.Base.Exceptions;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.Data;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bS.Sked2.Engine.Objects
{
    public abstract class EngineModule : BaseEngineComponent, IEngineFlowComponent, IEngineModule
    {
        protected readonly IUnitOfWork uow;
        protected readonly IEngineRepository engineRepository;
        protected IModuleEntry moduleEntry;


        /// <summary>
        /// The input properties
        /// </summary>
        protected IList<IEngineElementProperty> inputProperties;

        /// <summary>
        /// The output properties
        /// </summary>
        protected IList<IEngineElementProperty> outputProperties;


        public EngineModule(
            ILogger logger, 
            IMessageService messageService, 
            IUnitOfWork uow,
            IEngineRepository engineRepository) : base(logger, messageService)
        {
            this.uow = uow;
            this.engineRepository = engineRepository;
            inputProperties = new List<IEngineElementProperty>();
            outputProperties = new List<IEngineElementProperty>();
        }

        public abstract string Key { get; }

        public bool IsInit { get; set; }


        public abstract void Init();

        /// <summary>
        /// Registers the input properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="mandatory"></param>
        /// <exception cref="EngineException">Invalid property fields provided.
        /// or
        /// Cannot add property for this element. The property with key {key} still exists.</exception>
        public void RegisterInputProperties(string key, string description, DataType dataType, bool mandatory = false)
        {
            if (key.IsNullOrWhiteSpace())
            {
                throw new EngineException(logger, $"Invalid property fields provided.");
            }

            if (inputProperties.Any(p => p.Key == key))
            {
                throw new EngineException(logger, $"Cannot add property for this element. The property with key {key} still exists.");
            }

            inputProperties.Add(new ElementProperty(key, description, dataType, mandatory));

            // Init default value

            var engineDataType = AssembliesExtensions.GetTypesImplementingInterface<IEngineData>()
              .FirstOrDefault(ed => (DataType)ed.GetProperty("DataTypeConst").GetValue(ed) == dataType);

            SetDataValue(EngineDataDirection.Input, key, (IEngineData)Activator.CreateInstance(engineDataType));
        }

        /// <summary>
        /// Registers the output properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="mandatory"></param>
        /// <exception cref="EngineException">Invalid property fields provided.
        /// or
        /// Cannot add property for this element. The property with key {key} still exists.</exception>
        public void RegisterOutputProperties(string key, string description, DataType dataType, bool mandatory = false)
        {
            if (key.IsNullOrWhiteSpace())
            {
                throw new EngineException(logger, $"Invalid property fields provided.");
            }

            if (outputProperties.Any(p => p.Key == key))
            {
                throw new EngineException(logger, $"Cannot add property for this element. The property with key {key} still exists.");
            }

            outputProperties.Add(new ElementProperty(key, description, dataType, mandatory));
            // Init default value

            var engineDataType = AssembliesExtensions.GetTypesImplementingInterface<IEngineData>()
              .FirstOrDefault(ed => (DataType)ed.GetProperty("DataTypeConst").GetValue(ed) == dataType);

            SetDataValue(EngineDataDirection.Output, key, (IEngineData)Activator.CreateInstance(engineDataType));
        }

        /// <summary>
        /// Sets the data value.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="propertyKey">The property key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="EngineException">No input property with key {propertyKey} has been registered for this element.
        /// or
        /// No output property with key {propertyKey} has been registered for this element.</exception>
        public void SetDataValue(EngineDataDirection direction, string propertyKey, IEngineData value)
        {
            switch (direction)
            {
                case EngineDataDirection.Input:
                    var ip = inputProperties.SingleOrDefault(p => p.Key == propertyKey);
                    var ipE = moduleEntry?.InputProperties?.SingleOrDefault(p => p.Key == propertyKey);

                    if (ip == null) throw new EngineException(logger, $"No input property with key {propertyKey} has been registered for this module.");
                    ip.Value = value;
                    if (ipE != null) ipE.Value = ip.Value.WriteToStringValue();
                    break;

                case EngineDataDirection.Output:
                    var op = outputProperties.SingleOrDefault(p => p.Key == propertyKey);
                    var opE = moduleEntry?.OutputProperties?.SingleOrDefault(p => p.Key == propertyKey);

                    if (op == null) throw new EngineException(logger, $"No output property with key {propertyKey} has been registered for this module.");
                    op.Value = value;
                    if (opE != null) opE.Value = op.Value.WriteToStringValue();
                    break;
            }
        }

        /// <summary>
        /// Sets the data value if empty.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="propertyKey">The property key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="EngineException">
        /// No input property with key {propertyKey} has been registered for this element.
        /// or
        /// No output property with key {propertyKey} has been registered for this element.
        /// </exception>
        public void SetDataValueIfEmpty(EngineDataDirection direction, string propertyKey, IEngineData value)
        {
            switch (direction)
            {
                case EngineDataDirection.Input:
                    var ip = inputProperties.SingleOrDefault(p => p.Key == propertyKey);
                    var ipE = moduleEntry.InputProperties.SingleOrDefault(p => p.Key == propertyKey);

                    if (ip == null) throw new EngineException(logger, $"No input property with key {propertyKey} has been registered for this module.");
                    if (!ip.Value.IsFilled)
                    {
                        ip.Value = value;
                        ipE.Value = ip.Value.WriteToStringValue();
                    }
                    break;

                case EngineDataDirection.Output:
                    var op = outputProperties.SingleOrDefault(p => p.Key == propertyKey);
                    var opE = moduleEntry.OutputProperties.SingleOrDefault(p => p.Key == propertyKey);

                    if (op == null) throw new EngineException(logger, $"No output property with key {propertyKey} has been registered for this module.");
                    if (!op.Value.IsFilled)
                    {
                        op.Value = value;
                        opE.Value = op.Value.WriteToStringValue();
                    }
                    break;
            }
        }

        public void SetDataValueIfEmpty(EngineDataDirection direction, string propertyKey, DataType dataType, string persistingValue)
        {

            var engineDataType = AssembliesExtensions.GetTypesImplementingInterface<IEngineData>()
            .FirstOrDefault(ed => (DataType)ed.GetProperty("DataTypeConst").GetValue(ed) == dataType);

            var engineDataValue = (IEngineData)Activator.CreateInstance(engineDataType);

            engineDataValue.ReadFromStringValue(persistingValue);

            SetDataValueIfEmpty(direction, propertyKey, engineDataValue);
        }


        /// <summary>
        /// Gets the data value.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="propertyKey">The property key.</param>
        /// <returns></returns>
        /// <exception cref="EngineException">No input property with key {propertyKey} has been registered for this element.
        /// or
        /// No output property with key {propertyKey} has been registered for this element.</exception>
        public IEngineData GetDataValue(EngineDataDirection direction, string propertyKey)
        {
            switch (direction)
            {
                case EngineDataDirection.Input:
                    var ip = inputProperties.SingleOrDefault(p => p.Key == propertyKey);
                    if (ip == null) throw new EngineException(logger, $"No input property with key {propertyKey} has been registered for this module.");
                    return ip.Value;

                case EngineDataDirection.Output:
                    var op = outputProperties.SingleOrDefault(p => p.Key == propertyKey);
                    if (op == null) throw new EngineException(logger, $"No output property with key {propertyKey} has been registered for this module.");
                    return op.Value;
            }
            return null;
        }

        public override void LoadFromEntity(Guid EntityId)
        {
            moduleEntry = engineRepository.GetModuleById(EntityId);

            foreach (var inputProperty in moduleEntry.InputProperties)
            {
                SetDataValueIfEmpty(EngineDataDirection.Input, inputProperty.Key, inputProperty.DataType, inputProperty.Value);
            }

            foreach (var outputProperty in moduleEntry.OutputProperties)
            {
                SetDataValueIfEmpty(EngineDataDirection.Output, outputProperty.Key, outputProperty.DataType, outputProperty.Value);
            }
        }


    }
}
