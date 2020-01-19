using bs.Data;
using bs.Data.Interfaces;
using bS.Sked2.Engine.Objects;
using bS.Sked2.Model.Repositories;
using bS.Sked2.Service.Message;
using bS.Sked2.Service.Storage;
using bS.Sked2.Service.Validation;
using bS.Sked2.Shared;
using bS.Sked2.Structure.Base.Exceptions;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace bS.Sked2.Engine
{
    public class Engine : IEngine
    {
        protected IServiceProvider serviceProvider;
        private readonly ILogger logger;
        private readonly IDbContext dbContext;
        private readonly IServiceCollection services;

        public IEngineConfig Configuration { get; }
        public IServiceProvider ServiceProvider => serviceProvider;

        public Engine(ILogger logger, IDbContext dbContext, IEngineConfig configuration)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            Configuration = configuration;
            this.services = new ServiceCollection();
        }

        /// <summary>
        /// Executes the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <exception cref="EngineException">
        /// The element '{element.Name}'  cannot be executed. This task was aborted.
        /// or
        /// The element {element.Name} (id: {element.InstanceID}) was not executed cause one or more errors occurs. This task was aborted.
        /// or
        /// The element {element.Name} (id: {element.InstanceID}) was executed but one or more warning occurs. This task was aborted.
        /// </exception>
        public void ExecuteElement(IEngineElement element)
        {
            // Check if element can be executed
            if (!element.CanBeExecuted())
            {
                element.AddMessage($"The element cannot be executed.", MessageSeverity.Error);
                if (element.ParentTask.FailIfAnyElementHasError)
                {
                    throw new EngineException(logger, $"The element cannot be executed. This task was aborted.");
                }
                else
                {
                    return;
                }
            }

            // Execute element
            element.Start();

            // stop element
            element.Stop();

            if (element.HasErrors)
            {
                element.AddMessage($"The element (instance: {element.InstanceID}) was not executed cause one or more errors occurs.", MessageSeverity.Error);
                if (element.ParentTask.FailIfAnyElementHasError)
                {
                    throw new EngineException(logger, $"The element (instance: {element.InstanceID}) was not executed cause one or more errors occurs. This task was aborted.");
                }
            }
            else if (element.HasWarnings)
            {
                element.AddMessage($"The element (instance: {element.InstanceID}) was executed but one or more warning occurs.", MessageSeverity.Warning);
                if (element.ParentTask.FailIfAnyElementHasWarning)
                {
                    throw new EngineException(logger, $"The element (instance: {element.InstanceID}) was executed but one or more warning occurs. This task was aborted.");
                }
            }
        }

        public void ExecuteJob(Guid jobId)
        {
            var engineJob = serviceProvider.GetService<IEngineJob>();
            engineJob.LoadFromEntity(jobId);
            ExecuteJob(engineJob);
        }

        /// <summary>
        /// Executes the job.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ExecuteJob(IEngineJob job)
        {
            // Check if task can be executed
            if (!job.CanBeExecuted())
            {
                job.AddMessage($"The job cannot be executed.", MessageSeverity.Error);
                throw new EngineException(logger, $"The job  cannot be executed. This job was aborted.");
            }

            // Execute job
            job.Start();

            // Stop tjobask
            job.Stop();

            if (job.HasErrors)
            {
                job.AddMessage($"The job (instance: {job.InstanceID}) was not executed cause one or more errors occurs.", MessageSeverity.Error);
                throw new EngineException(logger, $"The job (instance: {job.InstanceID}) was not executed cause one or more errors occurs. This job was aborted.");
            }
            else if (job.HasWarnings)
            {
                job.AddMessage($"The job (instance: {job.InstanceID}) was executed but one or more warning occurs.", MessageSeverity.Warning);
                throw new EngineException(logger, $"The job (instance: {job.InstanceID}) was executed but one or more warning occurs. This job was aborted.");
            }
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ExecuteTask(IEngineTask task)
        {
            // Check if task can be executed
            if (!task.CanBeExecuted())
            {
                task.AddMessage($"The task cannot be executed.", MessageSeverity.Error);
                if (task.ParentJob.FailIfAnyTaskHasError)
                {
                    throw new EngineException(logger, $"The task  cannot be executed. This job was aborted.");
                }
                else
                {
                    return;
                }
            }

            // Execute task
            task.Start();

            // Stop task
            task.Stop();

            if (task.HasErrors)
            {
                task.AddMessage($"The task (instance: {task.InstanceID}) was not executed cause one or more errors occurs.", MessageSeverity.Error);
                if (task.ParentJob.FailIfAnyTaskHasError)
                {
                    throw new EngineException(logger, $"The task (instance: {task.InstanceID}) was not executed cause one or more errors occurs. This job was aborted.");
                }
            }
            else if (task.HasWarnings)
            {
                task.AddMessage($"The task (instance: {task.InstanceID}) was executed but one or more warning occurs.", MessageSeverity.Warning);
                if (task.ParentJob.FailIfAnyTaskHasWarning)
                {
                    throw new EngineException(logger, $"The task (instance: {task.InstanceID}) was executed but one or more warning occurs. This job was aborted.");
                }
            }
        }

        public void Init()
        {
            services.AddSingleton(dbContext);
            services.AddSingleton(logger);

            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IEngineRepository, EngineRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();

            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<ISqlValidationService, SqlValidationService>();
            services.AddSingleton<IStorageService, StorageService>();
            services.AddSingleton(Configuration);
            services.AddSingleton<IEngine, Engine>();
            services.AddTransient<IEngineJob, EngineJob>();
            services.AddTransient<IEngineTask, EngineTask>();
            services.AddTransient<EngineLink>();

            // Register all extensions elements and modules types
            //var engineModuleTypes = AssembliesExtensions.GetTypesImplementingInterface<IEngineModule>();
            var engineModuleTypes = AssembliesExtensions.GetTypesImplementingInterface<IEngineModule>(new string[] { Configuration.ExtensionsFolder }, true);
            foreach (var engineModule in engineModuleTypes)
            {
                engineModule.GetMethod("RegisterModule").Invoke(null, new object[] { services });
            }

            // build the service providere used in Engine
            serviceProvider = services.BuildServiceProvider();
        }
    }
}