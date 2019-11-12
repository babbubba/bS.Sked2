using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Extensions.Logging;

namespace bS.Sked2.Structure.Base.Exceptions
{
    public class StorageException : BaseLoggableException
    {
        public StorageException(ILogger logger, string message) : base(logger, message)
        {
        }

        public StorageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public StorageException(ILogger logger, string message, int errorCode) : base(logger, message, errorCode)
        {
        }

        public StorageException(ILogger logger, string message, Exception innerException) : base(logger, message, innerException)
        {
        }

        public StorageException(ILogger logger, string message, Exception innerException, int errorCode) : base(logger, message, innerException, errorCode)
        {
        }
    }
}
