using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Base
{
    /// <summary>
    /// Represent a startable elemnet.
    /// </summary>
    public interface IStartable
    {
        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();
        /// <summary>
        /// Pauses this instance.
        /// </summary>
        void Pause();
        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
        /// <summary>
        /// Gets or sets the begin time.
        /// </summary>
        /// <value>
        /// The begin time.
        /// </value>
        DateTime BeginTime { get; set; }
        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        DateTime EndTime { get; set; }
    }
}
