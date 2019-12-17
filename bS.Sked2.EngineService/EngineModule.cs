using Autofac;
using Autofac.Core;
using bS.Sked2.Engine.Objects;
using bS.Sked2.Model.Repositories;
using bS.Sked2.Service.Message;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Service;

namespace bS.Sked2.EngineService
{
    internal class EngineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<EngineJob>().As<IEngineJob>();
            builder.RegisterType<EngineTask>().As<IEngineTask>();
            builder.RegisterType<EngineRepository>().As<IEngineRepository>();
            builder.RegisterType<Engine.Engine>().As<IEngine>();

        }
    }
}