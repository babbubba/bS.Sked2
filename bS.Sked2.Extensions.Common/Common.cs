using bs.Data.Interfaces;
using bS.Sked2.Engine.Objects;
using bS.Sked2.Extensions.Common.FlatFile;
using bS.Sked2.Extensions.Common.Models;
using bS.Sked2.Extensions.Common.SqlServer;
using bS.Sked2.Model.Service;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.Data;
using bS.Sked2.Structure.Engine.Data.Types;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace bS.Sked2.Extensions.Common
{
    public class Common : EngineModule
    {
        public IStorageService StorageService;

        public Common(ILogger<Engine.Engine> logger, IMessageService messageService, IUnitOfWork uow, IEngineRepository enginRepo, IStorageService storageService) : base(logger, messageService, uow, enginRepo)
        {
            RegisterInputProperties("WorkspacePath", "Source file path", DataType.VirtualPath, true);
            this.StorageService = storageService;
        }

        public override string Key => KeyConst;
        public static string KeyConst => "Common";
        public static string Name => "Common Module";
        public static string Description => "This module contains the common elements.";

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
            var workspaceRootPath = GetDataValue(EngineDataDirection.Input, "WorkspacePath").Get<VirtualPath>().Path;
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

        public override IEngineEntry GetEmptyEntity()
        {
            return new CommonModuleEntry();
        }
    }
}