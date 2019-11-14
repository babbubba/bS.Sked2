using bS.Sked2.Shared;
using bS.Sked2.Structure.Base.Exceptions;
using bS.Sked2.Structure.Engine.Data;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace bS.Sked2.Structure.Engine
{
    public abstract class BaseEngineElement : BaseEngineComponent, IEngineElement
    {
        #region Fields
 
        /// <summary>
        /// The input properties
        /// </summary>
        protected IList<IEngineElementProperty> inputProperties;
        /// <summary>
        /// The output properties
        /// </summary>
        protected IList<IEngineElementProperty> outputProperties;

        protected DateTime? beginTime;
        protected DateTime? endTime;
        protected bool isPaused;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the begin time.
        /// </summary>
        /// <value>
        /// The begin time.
        /// </value>
        public DateTime? BeginTime => beginTime;

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public DateTime? EndTime => endTime;


        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; protected set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; protected set; }

        /// <summary>
        /// Gets or sets the parent task.
        /// </summary>
        /// <value>
        /// The parent task.
        /// </value>
        public IEngineTask ParentTask { get; set; }

        /// <summary>
        /// Gets or sets the parent module.
        /// </summary>
        /// <value>
        /// The parent module.
        /// </value>
        public IEngineModule ParentModule { get; set; }

        /// <summary>
        /// Gets the data input collection for this element.
        /// </summary>
        /// <value>
        /// The data input.
        /// </value>
        protected IDictionary<string, IEngineData> DataInput { get; set; }
        /// <summary>
        /// Gets the data output collection for this element.
        /// </summary>
        /// <value>
        /// The data output.
        /// </value>
        protected IDictionary<string, IEngineData> DataOutput { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning => beginTime != null && !isPaused && endTime == null;

        /// <summary>
        /// Gets a value indicating whether this instance has completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has completed; otherwise, <c>false</c>.
        /// </value>
        public bool HasCompleted => beginTime != null && !isPaused && endTime != null;

        /// <summary>
        /// Gets a value indicating whether this instance is paused.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is paused; otherwise, <c>false</c>.
        /// </value>
        public bool IsPaused => isPaused;

        #endregion

        #region C.tor

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEngineElement"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="messageService">The message service.</param>
        public BaseEngineElement(ILogger logger, IMessageService messageService) : base(logger, messageService)
        {
            inputProperties = new List<IEngineElementProperty>();
            outputProperties = new List<IEngineElementProperty>();
        }
        #endregion

        #region IEngineElement Methods

        /// <summary>
        /// Gets the data value.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="propertyKey">The property key.</param>
        /// <returns></returns>
        /// <exception cref="EngineException">
        /// No input property with key {propertyKey} has been registered for this element.
        /// or
        /// No output property with key {propertyKey} has been registered for this element.
        /// </exception>
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
        /// <exception cref="EngineException">
        /// No input property with key {propertyKey} has been registered for this element.
        /// or
        /// No output property with key {propertyKey} has been registered for this element.
        /// </exception>
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
        /// Registers the input properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="mandatory"></param>
        /// <exception cref="EngineException">
        /// Invalid property fields provided.
        /// or
        /// Cannot add property for this element. The property with key {key} still exists.
        /// </exception>
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

            inputProperties.Add(new ElementProperty(key,description,dataType, mandatory));
        }

        /// <summary>
        /// Registers the output properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="mandatory"></param>
        /// <exception cref="EngineException">
        /// Invalid property fields provided.
        /// or
        /// Cannot add property for this element. The property with key {key} still exists.
        /// </exception>
        public void RegisterOutputProperties(string key, string description, DataType dataType, bool mandatory = false)
        {
            if (key.IsNullOrWhiteSpace() )
            {
                throw new EngineException(logger, $"Invalid property fields provided.");
            }

            if (outputProperties.Any(p => p.Key == key))
            {
                throw new EngineException(logger, $"Cannot add property for this element. The property with key {key} still exists.");
            }

            outputProperties.Add(new ElementProperty(key, description, dataType, mandatory));
        }
        #endregion

        #region Startable Methods
        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public virtual void Pause()
        {
            isPaused = true;
        }
        /// <summary>
        /// Starts this instance. In derived class you have to execute this base before your overrided code.
        /// </summary>
        public virtual void Start()
        {
            // Create the instance ID for this element
            instanceId = new Guid();

            // Set the execution begin time
            beginTime = DateTime.Now;

            // Add a message to notify the element started
            AddMessage("Element execution started.");
        }


        /// <summary>
        /// Stops this instance.
        /// </summary>
        public virtual void Stop()
        {
            // It set paused value to false in case this element was paused previously
            isPaused = false;

            // Set this element finish time
            endTime = DateTime.Now;

            // Add a message to notify the element finish execution
            AddMessage("Element execution finish.");
        }

        /// <summary>
        /// Determines whether this instance [can be executed]. It checks parents job, task and module and check mandatory input parameters.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can be executed]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanBeExecuted()
        {
            // Check for Job and Task statues
            if (ParentModule?.InstanceID == null)
            {
                AddMessage("Error starting element. No related Job instance found.", MessageSeverity.Error);
                return false;
            }
            if (ParentTask?.ParentJob?.InstanceID == null)
            {
                AddMessage("Error starting element. No related Job instance found.", MessageSeverity.Error);
                return false;

            }
            if (!ParentTask?.ParentJob?.IsRunning ?? false)
            {
                AddMessage("Error starting element. Related Job is not in running status.", MessageSeverity.Error);
                return false;

            }
            if (!ParentTask?.IsRunning ?? false)
            {
                AddMessage("Error starting element. Related Task is not in running status.", MessageSeverity.Error);
                return false;
            }

            // Check mandatory properties
            var isAnyNull = false;
            foreach (var property in inputProperties.Where(p => p.Mandatory))
            {
                var isNull = false;
                switch (property.DataType)
                {
                    case DataType.Int:
                        if (property.Value?.Get<int>() == null) isNull = true;
                        break;
                    case DataType.Bool:
                        if (property.Value?.Get<bool>() == null) isNull = true;
                        break;
                    case DataType.Decimal:
                        if (property.Value?.Get<decimal>() == null) isNull = true;
                        break;
                    case DataType.Double:
                        if (property.Value?.Get<double>() == null) isNull = true;
                        break;
                    case DataType.Char:
                        if (property.Value?.Get<char>() == null) isNull = true;
                        break;
                    case DataType.Datetime:
                        if (property.Value?.Get<DateTime>() == null) isNull = true;
                        break;
                    case DataType.Table:
                        if (property.Value?.Get<DataTable>() == null) isNull = true;
                        break;
                    case DataType.String:
                        if (((string)property.Value?.Get<string>() ?? null).IsNullOrWhiteSpace()) isNull = true;
                        break;
                }
                if (isNull)
                {
                    AddMessage($"Error starting element. Mandatory property '{property.Description}({property.Key})' is empty.", MessageSeverity.Error);
                    isAnyNull = true;
                }
            }
            return !isAnyNull;
        }
        #endregion Startable

    }
}