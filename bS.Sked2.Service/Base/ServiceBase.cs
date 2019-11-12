using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Service.Base
{
    public abstract class ServiceBase : Structure.Service.IService
    {
        protected readonly ILogger logger;
        public ServiceBase(ILogger logger)
        {
            this.logger = logger;
        }
    }
}
