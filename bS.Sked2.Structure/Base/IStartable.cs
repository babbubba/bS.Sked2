using System;

namespace bS.Sked2.Structure.Base
{
    /// <summary>
    /// Represent a startable elemnet.
    /// </summary>
    public interface IStartable
    {
        /// <summary>
        /// Gets or sets the begin time.
        /// </summary>
        /// <value>
        /// The begin time.
        /// </value>
        DateTime? BeginTime { get; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        DateTime? EndTime { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has completed; otherwise, <c>false</c>.
        /// </value>
        bool HasCompleted { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is paused.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is paused; otherwise, <c>false</c>.
        /// </value>
        bool IsPaused { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        bool IsRunning { get; }

        /// <summary>
        /// Determines whether this instance [can be executed].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can be executed]; otherwise, <c>false</c>.
        /// </returns>
        bool CanBeExecuted();

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        void Pause();

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
    }
}