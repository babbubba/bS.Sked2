using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace bS.Sked2.CompositionRoot.Interfaces

{
    public interface ICompositionRoot
    {
        bool BuildContainer();
        void Register<Component>();
        void Register<Component, Service>();
        void RegisterGeneric(Type component, Type service);
        void RegisterInstance<Component>(Component componentInstance) where Component : class;
        void RegisterInstance<Component, Service>(Component componentInstance) where Component : class;
        void RegisterIocModule<T>(T iocModule) where T : ICompositionRootModule;
        T Resolve<T>();
    }
}
