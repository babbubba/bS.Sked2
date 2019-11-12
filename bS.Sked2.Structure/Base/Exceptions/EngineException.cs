using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Extensions.Logging;

namespace bS.Sked2.Structure.Base.Exceptions
{
    /// <summary>
    /// The Engine exception.
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Base.Exceptions.BaseLoggableException" />
    public class EngineException : BaseLoggableException
    {
        public Guid JobInstanceId { get; }
        public EngineException(ILogger logger, string message) : base(logger, message)
        {
        }

        public EngineException(ILogger logger, string message, Guid jobInstanceId) : base(logger, message)
        {
            JobInstanceId = jobInstanceId;
        }

        public EngineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EngineException(ILogger logger, string message, int errorCode) : base(logger, message, errorCode)
        {
        }
        public EngineException(ILogger logger, string message, int errorCode, Guid jobInstanceId) : base(logger, message, errorCode)
        {
            JobInstanceId = jobInstanceId;
        }

        public EngineException(ILogger logger, string message, Exception innerException) : base(logger, message, innerException)
        {
        }
        public EngineException(ILogger logger, string message, Exception innerException, Guid jobInstanceId) : base(logger, message, innerException)
        {
            JobInstanceId = jobInstanceId;
        }

        public EngineException(ILogger logger, string message, Exception innerException, int errorCode) : base(logger, message, innerException, errorCode)
        {
        }
        public EngineException(ILogger logger, string message, Exception innerException, int errorCode, Guid jobInstanceId) : base(logger, message, innerException, errorCode)
        {
            JobInstanceId = jobInstanceId;
        }
    }
}
