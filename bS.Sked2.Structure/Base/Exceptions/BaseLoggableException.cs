using Microsoft.Extensions.Logging;
using System;
using System.Runtime.Serialization;

namespace bS.Sked2.Structure.Base.Exceptions
{
    /// <summary>
    /// This is the base abstracded class for all exception. It automatically manages log messages when user throw created exception based on this and permits to manage an error code integer value.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public abstract class BaseLoggableException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLoggableException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BaseLoggableException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLoggableException"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        public BaseLoggableException(ILogger logger, string message) : base(message)
        {
            logger.LogError(message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLoggableException"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="errorCode">The error code.</param>
        public BaseLoggableException(ILogger logger, string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
            logger.LogError($"[Err: {errorCode}] - {message}");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLoggableException"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public BaseLoggableException(ILogger logger, string message, Exception innerException) : base(message, innerException)
        {
            logger.LogError(innerException, message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLoggableException"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="errorCode">The error code.</param>
        public BaseLoggableException(ILogger logger, string message, Exception innerException, int errorCode) : base(message, innerException)
        {
            ErrorCode = errorCode;
            logger.LogError(innerException, $"[Err: {errorCode}] - {message}");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLoggableException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected BaseLoggableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public int ErrorCode { get; private set; }
    }
}