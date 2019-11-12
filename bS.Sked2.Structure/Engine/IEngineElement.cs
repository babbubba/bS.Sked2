using bS.Sked2.Structure.Base;
using bS.Sked2.Structure.Base.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// E' l'elemento che rappresenta una operazione specifica in un Task. Ogni elemento viene eseguito dal suo specifico modulo <see cref="IEngineModule"/>.
    /// </summary>
    public interface IEngineElement : IStartable
    {
        /// <summary>
        /// Gets the data input value from the data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <returns></returns>
        IEngineData GetDataInputValue(string dataKey);
        /// <summary>
        /// Gets the data output value from the data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <returns></returns>
        IEngineData GetDataOutputValue(string dataKey);

        /// <summary>
        /// Sets the data input value for the specified the data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <param name="value">The value.</param>
        void SetDataInputValue(string dataKey, IEngineData value);
        /// <summary>
        /// Sets the data output value for the specified the data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <param name="value">The value.</param>
        void SetDataOutputValue(string dataKey, IEngineData value);
        /// <summary>
        /// Gets the parent task.
        /// </summary>
        /// <value>
        /// The parent task.
        /// </value>
        IEngineTask ParentTask { get; }


    }

    public abstract class BaseEngineElement : IEngineElement
    {
        private readonly ILogger logger;

        public BaseEngineElement(ILogger logger)
        {
            this.logger = logger;
        }
        public DateTime BeginTime { get; protected set; }
        public DateTime EndTime { get; protected set; }
        public IEngineTask ParentTask { get; protected set; }

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
        /// Pauses this instance.
        /// </summary>
        public virtual void Pause()
        { 
        }

        /// <summary>
        /// Sets the data input value for the specified data key.
        /// </summary>
        /// <param name="dataKey">The data key.</param>
        /// <param name="value">The value.</param>
        public void SetDataInputValue(string dataKey, IEngineData value)
        {
            if (DataInput.ContainsKey(dataKey))  DataInput[dataKey] = value;
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
            EndTime =  DateTime.Now;
        }
    }
}
