using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace bS.Sked2.CompositionRoot
{
    public class CompositionRoot //: ICompositionRoot
    {
        private readonly ContainerBuilder builder;
        private readonly IServiceCollection services;

        public CompositionRoot(IServiceCollection services)
        {
            builder = new ContainerBuilder();
            this.services = services;

            builder.Populate(services);
        }

        public T Resolve<T>()
        {
            throw new NotImplementedException();
        }
        //public IServiceCollection AddCompositionRoot(this IServiceCollection services)
        //{
        //    if (services == null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }

        //    //services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
        //    //services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));

        //    return services;
        //}

        //public void BuildContainer()
        //{
        //        //iocContainer = builder.Build();
        //}

        //public IServiceProvider GetServiceProder()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Register<Component>()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Register<Component, Service>()
        //{
        //    throw new NotImplementedException();
        //}

        //public void RegisterExtensionsAssemblyTypes(Assembly assembly)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RegisterGeneric(Type component, Type service)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RegisterInstance<Component>(Component componentInstance) where Component : class
        //{
        //    throw new NotImplementedException();
        //}

        //public void RegisterInstance<Component, Service>(Component componentInstance) where Component : class
        //{
        //    throw new NotImplementedException();
        //}

        //public void RegisterIocModule<T>(T iocModule) where T : ICompositionRootModule
        //{
        //    throw new NotImplementedException();
        //}


    }
}
