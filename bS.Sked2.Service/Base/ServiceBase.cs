using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Service.Base
{
    /// <summary>
    /// This is the based service class. Every Services have to implement this abstracted class.
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Service.IService" />
    public abstract class ServiceBase : Structure.Service.IService
    {
        protected readonly ILogger logger;
        public ServiceBase(ILogger logger)
        {
            this.logger = logger;
        }
    }
}
