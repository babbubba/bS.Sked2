using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace bS.Sked2.Structure.Base.Exceptions
{
    /// <summary>
    /// This is the base abstracded class for all exception. It automatically manages log messages when user throw created exception based on this and permits to manage an error code integer value.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public abstract class BaseLoggableException : Exception
    {
        public int ErrorCode { get; private set; }
        public BaseLoggableException(ILogger logger, string message) : base(message)
        {
            logger.LogError(message);
        }

        public BaseLoggableException(ILogger logger, string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
            logger.LogError($"[Err: {errorCode}] - {message}");
        }

        public BaseLoggableException(ILogger logger, string message, Exception innerException) : base(message, innerException)
        {
            logger.LogError(innerException, message);
        }

        public BaseLoggableException(ILogger logger, string message, Exception innerException, int errorCode) : base(message, innerException)
        {
            ErrorCode = errorCode;
            logger.LogError(innerException, $"[Err: {errorCode}] - {message}");
        }

        protected BaseLoggableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
