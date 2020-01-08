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

namespace bS.Sked2.Engine.Objects
{
    public abstract class EngineElement : BaseEngineComponent, IEngineElement
    {
        protected readonly IEngineRepository engineRepository;
        protected IElementEntry elementEntry;

        /// <summary>
        /// The input properties
        /// </summary>
        protected IList<IEngineElementProperty> inputProperties;

        /// <summary>
        /// The output properties
        /// </summary>
        protected IList<IEngineElementProperty> outputProperties;

        protected readonly IUnitOfWork uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineElement"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="enginRepo">The engin repo.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="messageService">The message service.</param>
        public EngineElement(
            IUnitOfWork uow,
            IEngineRepository enginRepo,
            ILogger logger,
            IMessageService messageService) : base(logger, messageService)
        {
            this.uow = uow;
            this.engineRepository = enginRepo;
            inputProperties = new List<IEngineElementProperty>();
            outputProperties = new List<IEngineElementProperty>();
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public abstract string Key { get; }
        public IModuleEntry ParentModule { get => elementEntry.ParentModule; }
        public ITaskEntry ParentTask { get => elementEntry.ParentTask; }

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
                    var ipE = elementEntry?.InputProperties?.SingleOrDefault(p => p.Key == propertyKey);

                    if (ip == null) throw new EngineException(logger, $"No input property with key {propertyKey} has been registered for this element.");
                    ip.Value = value;
                   if(ipE!=null) ipE.Value = ip.Value.WriteToStringValue();
                    break;

                case EngineDataDirection.Output:
                    var op = outputProperties.SingleOrDefault(p => p.Key == propertyKey);
                    var opE = elementEntry?.OutputProperties?.SingleOrDefault(p => p.Key == propertyKey);

                    if (op == null) throw new EngineException(logger, $"No output property with key {propertyKey} has been registered for this element.");
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
                    var ipE = elementEntry.InputProperties.SingleOrDefault(p => p.Key == propertyKey);

                    if (ip == null) throw new EngineException(logger, $"No input property with key {propertyKey} has been registered for this element.");
                    if (!ip.Value.IsFilled)
                    {
                        ip.Value = value;
                        ipE.Value = ip.Value.WriteToStringValue();
                    }
                    break;

                case EngineDataDirection.Output:
                    var op = outputProperties.SingleOrDefault(p => p.Key == propertyKey);
                    var opE = elementEntry.OutputProperties.SingleOrDefault(p => p.Key == propertyKey);

                    if (op == null) throw new EngineException(logger, $"No output property with key {propertyKey} has been registered for this element.");
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
        /// Starts this instance.
        /// </summary>
        public override void Start()
        {
            uow.BeginTransaction();

            // Create the instance ID for this element
            instance = engineRepository.CreateNewInstance();

            // Set the execution begin time
            instance.BeginTime = DateTime.Now;

            // Add current instance to entry
            elementEntry.Instances.Add(instance);

            uow.Commit();

            // Add a message to notify the element started
            AddMessage("Element execution started.");
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

            uow.Commit();
            // Add a message to notify the element finish execution
            AddMessage("Element execution finish.");
        }

        public override void LoadFromEntity(Guid EntityId)
        {
            elementEntry = engineRepository.GetElementById(EntityId);

            foreach (var inputProperty in elementEntry.InputProperties)
            {
                SetDataValueIfEmpty(EngineDataDirection.Input, inputProperty.Key, inputProperty.DataType, inputProperty.Value);
            }

            foreach (var outputProperty in elementEntry.OutputProperties)
            {
                SetDataValueIfEmpty(EngineDataDirection.Output, outputProperty.Key, outputProperty.DataType, outputProperty.Value);
            }
        }
    }
}