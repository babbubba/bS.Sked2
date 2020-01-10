﻿using Microsoft.Extensions.Logging;
using System;
using System.Runtime.Serialization;

namespace bS.Sked2.Structure.Base.Exceptions
{
    /// <summary>
    /// Exception generated by Storage Service.
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Base.Exceptions.BaseLoggableException" />
    public class StorageException : BaseLoggableException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public StorageException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageException"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        public StorageException(ILogger logger, string message) : base(logger, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        public StorageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageException"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="errorCode">The error code.</param>
        public StorageException(ILogger logger, string message, int errorCode) : base(logger, message, errorCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageException"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public StorageException(ILogger logger, string message, Exception innerException) : base(logger, message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageException"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="errorCode">The error code.</param>
        public StorageException(ILogger logger, string message, Exception innerException, int errorCode) : base(logger, message, innerException, errorCode)
        {
        }
    }
}