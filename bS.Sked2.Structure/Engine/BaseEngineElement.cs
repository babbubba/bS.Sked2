using bS.Sked2.Structure.Base.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace bS.Sked2.Structure.Engine
{
    public abstract class BaseEngineElement : IEngineElement
    {
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger logger;
        /// <summary>
        /// Gets or sets the begin time.
        /// </summary>
        /// <value>
        /// The begin time.
        /// </value>
        public DateTime BeginTime { get; protected set; }
        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public DateTime EndTime { get; protected set; }
        /// <summary>
        /// Gets or sets the parent task.
        /// </summary>
        /// <value>
        /// The parent task.
        /// </value>
        public IEngineTask ParentTask { get; protected set; }
        /// <summary>
        /// Gets or sets the parent module.
        /// </summary>
        /// <value>
        /// The parent module.
        /// </value>
        public IEngineModule ParentModule { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEngineElement"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="parentTask">The parent task.</param>
        /// <param name="parentModule">The parent module.</param>
        public BaseEngineElement(ILogger logger, IEngineTask parentTask, IEngineModule parentModule)
        {
            this.logger = logger;
            ParentTask = parentTask;
            ParentModule = parentModule;
        }

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
        /// Gets the data input value from the data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <returns></returns>
        /// <exception cref="bS.Sked2.Structure.Base.Exceptions.EngineException">Cannot find data imput value for key: '{dataKey}'.</exception>
        public IEngineData GetDataInputValue(string dataKey)
        {
            if (DataInput.ContainsKey(dataKey)) return DataInput[dataKey];
            throw new EngineException(logger,"Cannot find data imput value for key: '{dataKey}'.");
        }

        /// <summary>
        /// Gets the data output value from the data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <returns></returns>
        /// <exception cref="bS.Sked2.Structure.Base.Exceptions.EngineException">Cannot find data output value for key: '{dataKey}'.</exception>
        public IEngineData GetDataOutputValue(string dataKey)
        {
            if (DataOutput.ContainsKey(dataKey)) return DataOutput[dataKey];
            throw new EngineException(logger, "Cannot find data output value for key: '{dataKey}'.");
        }

        /// <summary>
        /// Sets the data input value for the specified data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <param name="value">The value.</param>
        public void SetDataInputValue(string dataKey, IEngineData value)
        {
            if (DataInput.ContainsKey(dataKey)) DataInput[dataKey] = value;
            DataInput.Add(dataKey, value);
        }

        /// <summary>
        /// Sets the data output value for the specified data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <param name="value">The value.</param>
        public void SetDataOutputValue(string dataKey, IEngineData value)
        {
            if (DataOutput.ContainsKey(dataKey)) DataOutput[dataKey] = value;
            DataOutput.Add(dataKey, value);
        }

        #region Startable
        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public virtual void Pause()
        {
        }
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public virtual void Start()
        {
            BeginTime = DateTime.Now;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public virtual void Stop()
        {
            EndTime = DateTime.Now;
        }
        #endregion Startable
    }
}
