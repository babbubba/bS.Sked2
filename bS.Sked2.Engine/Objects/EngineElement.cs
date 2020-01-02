using bs.Data.Interfaces;
using bS.Sked2.Shared;
using bS.Sked2.Structure.Base.Exceptions;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.Data;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bS.Sked2.Engine.Objects
{
    public abstract class EngineElement : BaseEngineComponent, IEngineElement
    {
        private readonly IUnitOfWork uow;
        protected readonly IEngineRepository engineRepository;
        protected IElementEntity elementEntry;
        public EngineElement(
            IUnitOfWork uow,
            IEngineRepository enginRepo,
            ILogger<EngineElement> logger,
            IMessageService messageService) : base(logger, messageService)
        {
            this.uow = uow;
            this.engineRepository = enginRepo;
        }

        /// <summary>
        /// The input properties
        /// </summary>
        protected IList<IEngineElementProperty> inputProperties;
        /// <summary>
        /// The output properties
        /// </summary>
        protected IList<IEngineElementProperty> outputProperties;

        public ITaskEntry ParentTask { get => elementEntry.ParentTask; }
        public IEngineModule ParentModule { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public abstract string Key { get; }

        /// <summary>
        /// Determines whether this instance [can be executed].
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance [can be executed]; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanBeExecuted()
        {
            // TODO: Add logic here if needed
            return true;
        }

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

            // Init default value
            inputProperties.Add(new ElementProperty(key, description, dataType, mandatory));
            switch (dataType)
            {
                case DataType.Int:
                    SetDataValue(EngineDataDirection.Input, key, new IntValue());
                    break;
                case DataType.Bool:
                    SetDataValue(EngineDataDirection.Input, key, new BoolValue());
                    break;
                case DataType.Decimal:
                    SetDataValue(EngineDataDirection.Input, key, new DecimalValue());
                    break;
                case DataType.Double:
                    SetDataValue(EngineDataDirection.Input, key, new DoubleValue());
                    break;
                case DataType.Char:
                    SetDataValue(EngineDataDirection.Input, key, new CharValue());
                    break;
                case DataType.String:
                    SetDataValue(EngineDataDirection.Input, key, new StringValue());
                    break;
                case DataType.Datetime:
                    SetDataValue(EngineDataDirection.Input, key, new DateTimeValue());
                    break;
                case DataType.Table:
                    SetDataValue(EngineDataDirection.Input, key, new TableValue());
                    break;
                case DataType.DictionaryEntry:
                    SetDataValue(EngineDataDirection.Input, key, new DictionaryEntryValue());
                    break;
                case DataType.Collection:
                    SetDataValue(EngineDataDirection.Input, key, new CollectionValue());
                    break;
            }

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
                    if (ip == null) throw new EngineException(logger, $"No input property with key {propertyKey} has been registered for this element.");
                    return ip.Value;
                case EngineDataDirection.Output:
                    var op = outputProperties.SingleOrDefault(p => p.Key == propertyKey);

                    if (op == null) throw new EngineException(logger, $"No output property with key {propertyKey} has been registered for this element.");
                    return op.Value;
            }
            return null;
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
                    if (ip == null) throw new EngineException(logger, $"No input property with key {propertyKey} has been registered for this element.");
                    ip.Value = value;
                    break;
                case EngineDataDirection.Output:
                    var op = outputProperties.SingleOrDefault(p => p.Key == propertyKey);

                    if (op == null) throw new EngineException(logger, $"No output property with key {propertyKey} has been registered for this element.");
                    op.Value = value;
                    break;
            }
        }

    
        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public override void Pause()
        {
            uow.BeginTransaction();

            instance.IsPaused = true;

            AddMessage("Element execution paused.");

            uow.Commit();
        }

     

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public override void Start()
        {
            uow.BeginTransaction();

            // Create the instance ID for this element
            instance = engineRepository.CreateNewInstance();

            // Set the execution begin time
            instance.BeginTime = DateTime.Now;

            // Add a message to notify the element started
            AddMessage("Element execution started.");

            uow.Commit();

            //TODO: Execute Job Logic

        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            uow.BeginTransaction();

            // It set paused value to false in case this element was paused previously
            instance.IsPaused = false;

            // instance this element finish time
            instance.EndTime = DateTime.Now;

            // Add a message to notify the element finish execution
            AddMessage("Element execution finish.");

            uow.Commit();
        }
    }
}
