//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Text;

//namespace bS.Sked2.CompositionRoot
//{
//    public interface ICompositionRoot
//    {
//        /// <summary>
//        /// Builds the DI container.
//        /// </summary>
//        void BuildContainer();
//        //IDependencyResolver GetMvcDependencyResolver();
//        /// <summary>
//        /// Gets the ServiceProvider object used for example in startup's ConfigureServices method.
//        /// </summary>
//        /// <returns></returns>
//        IServiceProvider GetServiceProder();
//        void Register<Component>();
//        void Register<Component, Service>();
//        void RegisterExtensionsAssemblyTypes(Assembly assembly);
//        //void RegisterControllers(Assembly mvcAssembly);
//        //void RegisterFilterProvider();
//        void RegisterGeneric(Type component, Type service);
//        void RegisterInstance<Component>(Component componentInstance) where Component : class;
//        void RegisterInstance<Component, Service>(Component componentInstance) where Component : class;
//        /// <summary>
//        /// Registers a module with a list of components and/or services registered.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="iocModule">The ioc module.</param>
//        void RegisterIocModule<T>(T iocModule) where T : ICompositionRootModule;
//        //void RegisterModelBinderProvider();
//        //void RegisterModelBinders(Assembly mvcAssembly);
//        //void RegisterView();
//        //void RegisterWebAbstractionModule();
//        T Resolve<T>();
//    }
//}
