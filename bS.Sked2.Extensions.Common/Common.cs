using bs.Data.Interfaces;
using bS.Sked2.Engine.Objects;
using bS.Sked2.Extensions.Common.FlatFile;
using bS.Sked2.Extensions.Common.SqlServer;
using bS.Sked2.Model.Service;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Extensions.Common
{
    public class Common : EngineModule
    {
        public IStorageService StorageService;

        public Common(ILogger logger, IMessageService messageService, IUnitOfWork uow, IEngineRepository enginRepo, IStorageService storageService) : base(logger, messageService, uow, enginRepo)
        {
            RegisterInputProperties("WorkspacePath", "Source file path", DataType.String, true);
            this.StorageService = storageService;
        }

        public override string Key => "Common";
        public static string KeyConst => "Common";

        public static void RegisterModule(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<Common>();
            serviceCollection.AddTransient<FlatFileReader>();
            serviceCollection.AddTransient<FlatFileWriter>();
            serviceCollection.AddTransient<SqlQueryReader>();
            serviceCollection.AddTransient<SqlTableWriter>();
        }

        public override bool CanBeExecuted()
        {
            return IsInit == true;
        }

        public override void Init()
        {
            var workspaceRootPath = GetDataValue(EngineDataDirection.Input, "WorkspacePath").Get<string>();
            StorageService.LoadConfig(new StorageServiceConfig(workspaceRootPath));
            IsInit = true;
        }

        public override void Pause()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
