using bs.Data.Interfaces;
using bS.Sked2.Shared;
using bS.Sked2.Structure.Base.Exceptions;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using bS.Sked2.Structure.Service.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bS.Sked2.Engine.Objects
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Engine.BaseEngineComponent" />
    /// <seealso cref="bS.Sked2.Structure.Engine.IEngineTask" />
    public class EngineTask : BaseEngineComponent, IEngineTask
    {
        /// <summary>
        /// The engine
        /// </summary>
        private readonly IEngine engine;

        /// <summary>
        /// The engine repository
        /// </summary>
        private readonly IEngineRepository engineRepository;

        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// The uow
        /// </summary>
        private readonly IUnitOfWork uow;

        /// <summary>
        /// The task entry
        /// </summary>
        private ITaskEntry taskEntry;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineTask"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="engineRepository">The engine repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="messageService">The message service.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public EngineTask(
            IUnitOfWork uow,
            IEngineRepository engineRepository,
            ILogger<EngineTask> logger,
            IMessageService messageService,
            IEngine engine,
            IServiceProvider serviceProvider) : base(logger, messageService)
        {
            this.uow = uow;
            this.engineRepository = engineRepository;
            this.engine = engine;
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the parent Job.
        /// </summary>
        /// <value>
        /// The parent task.
        /// </value>
        public IJobEntry ParentJob { get => taskEntry.ParentJob; }

        public override Guid? EntityId => taskEntry?.Id;

        /// <summary>
        /// Determines whether this instance [can be executed].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can be executed]; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanBeExecuted()
        {
            // TODO: Add logic here if needed
            return true;
        }

        public override IEngineEntry GetEmptyEntity()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads from entity.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        public override void LoadFromEntity(Guid taskId)
        {
            taskEntry = engineRepository.GetTaskById(taskId);
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public override void Pause()
        {
            using (var transaction = uow.BeginTransaction())
            {
                instance.IsPaused = true;
            }

            AddMessage("Task execution paused.");
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public override void Start()
        {
            using (var transaction = uow.BeginTransaction())
            {
                // Create the instance ID for this element
                instance = engineRepository.CreateNewInstance();

                // Set the execution begin time
                instance.BeginTime = DateTime.Now;

                // Add current instance to entry
                taskEntry.Instances.Add(instance);
            }

            // Add a message to notify the element started
            AddMessage("Task execution started.");

            // Creating the engine elements flow
            var engineElementsFlow = new List<IEngineElement>();
            GetEngineTaskElementsFlow(engineElementsFlow);

            AddMessage("Elements flow created.", MessageSeverity.Debug);

            // now execute the elements flow
            for (var elementIdx = 0; elementIdx < engineElementsFlow.Count; elementIdx++)
            {
                // Set temporary variable for convenience
                var currentElement = engineElementsFlow[elementIdx];

                // Check if is a link element
                if (currentElement.Key == "ElementsLink")
                {
                    ExecuteElementLinkLogic(engineElementsFlow, elementIdx, currentElement);
                    // no more logic for this element... continue to next element
                    continue;
                }

                // If we are here we need to execute this element
                try
                {
                    engine.ExecuteElement(currentElement);
                }
                catch (EngineException eEx)
                {
                    // handled exception
                    //TODO: Handle this
                    throw;
                }
                catch (Exception ex)
                {
                    // unhandled exception
                    //TODO: Handle this

                    throw;
                }
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            using (var transaction = uow.BeginTransaction())
            {
                // It set paused value to false in case this element was paused previously
                instance.IsPaused = false;

                // Set this element finish time
                instance.EndTime = DateTime.Now;

                // Add a message to notify the element finish execution
                AddMessage("Task execution finish.");
            }
        }

        /// <summary>
        /// Executes the element link logic.
        /// </summary>
        /// <param name="engineElementsFlow">The engine elements flow.</param>
        /// <param name="elementIdx">Index of the element.</param>
        /// <param name="currentElement">The current element.</param>
        /// <exception cref="EngineException">
        /// </exception>
        private void ExecuteElementLinkLogic(List<IEngineElement> engineElementsFlow, int elementIdx, IEngineElement currentElement)
        {
            // check if previous and next elements are possible
            if (elementIdx - 1 < 0 && elementIdx + 1 >= engineElementsFlow.Count)
            {
                // error! There is a problem with the elements flow
                var errorMessage = $"The elements flow is not correct! Verifiy element before and after link elements.";
                AddMessage(errorMessage, MessageSeverity.Fatal);
                throw new EngineException(logger, errorMessage, (Guid)InstanceID);
            }

            // Check if previous element is the right one
            if (engineElementsFlow[elementIdx - 1].Key != ((EngineLink)currentElement).PreviuousElement.Key)
            {
                // error! There is a problem with the elements flow
                var errorMessage = $"The elements flow is not correct! The element before the link is not the expected type.";
                AddMessage(errorMessage, MessageSeverity.Fatal);
                throw new EngineException(logger, errorMessage, (Guid)InstanceID);
            }

            // Check if next element is the right one
            if (engineElementsFlow[elementIdx + 1].Key != ((EngineLink)currentElement).NextElement.Key)
            {
                // error! There is a problem with the elements flow
                var errorMessage = $"The elements flow is not correct! The element after the link is not the expected type.";
                AddMessage(errorMessage, MessageSeverity.Fatal);
                throw new EngineException(logger, errorMessage, (Guid)InstanceID);
            }

            // ok I can map data from previous element to next one
            foreach (var mapping in ((EngineLink)currentElement).Mappings)
            {
                try
                {
                    var previousOutput = engineElementsFlow[elementIdx - 1].GetDataValue(EngineDataDirection.Output, mapping.InputPropertyKey);
                    engineElementsFlow[elementIdx + 1].SetDataValue(EngineDataDirection.Input, mapping.OutputPropertyKey, previousOutput);
                }
                catch (Exception ex)
                {
                    var errorMessage = $"Error passing data between elements.";
                    AddMessage(errorMessage, MessageSeverity.Fatal);
                    throw new EngineException(logger, errorMessage, ex, (Guid)InstanceID);
                }
            }
        }

        /// <summary>
        /// Gets the engine task elements flow.
        /// </summary>
        /// <param name="engineElementsFlow">The engine elements flow.</param>
        private void GetEngineTaskElementsFlow(List<IEngineElement> engineElementsFlow)
        {
            var modules = new Dictionary<Guid, IEngineModule>();

            // fetch all entities element
            foreach (var elementEntry in taskEntry.Elements.OrderBy(e => e.Position))
            {
                // finding parent module in current loaded modules (not all element have a parent module)
                if (elementEntry.ParentModule != null && !modules.ContainsKey(elementEntry.ParentModule.Id))
                {
                    //we need to load and init this module
                    // finding the EngineElement to execute this entry
                    //var engineModuleType = AssembliesExtensions.GetTypesImplementingInterface<IEngineModule>()
                    var engineModuleType = AssembliesExtensions.GetTypesImplementingInterface<IEngineModule>(new string[] { engine.Configuration.ExtensionsFolder }, true)
                        .FirstOrDefault(ed => (string)ed.GetProperty("KeyConst")?.GetValue(ed) == elementEntry.ParentModule.Key);

                    if (engineModuleType == null)
                    {
                        // we cant find a valid module implementation
                        throw new EngineException(logger, $"Cannot find a valid implementation for the module with key: '{elementEntry.ParentModule.Key}'.");
                    }

                    // create new instance of Engine Module
                    var engineModule = (IEngineModule)serviceProvider.GetService(engineModuleType);

                    // Load module's data
                    engineModule.LoadFromEntity(elementEntry.ParentModule.Id);

                    // init the module
                    engineModule.Init();

                    modules.Add(elementEntry.ParentModule.Id, engineModule);
                }

                // finding the EngineElement to execute this entry
                //var engineElementType = AssembliesExtensions.GetTypesImplementingInterface<IEngineElement>()
                var engineElementType = AssembliesExtensions.GetTypesImplementingInterface<IEngineElement>(new string[] { engine.Configuration.ExtensionsFolder }, true)
                    .FirstOrDefault(ed => (string)ed.GetProperty("KeyConst")?.GetValue(ed) == elementEntry.Key);

                if (engineElementType == null)
                {
                    // we cant find a valid module implementation
                    throw new EngineException(logger, $"Cannot find a valid implementation for the element with key: '{elementEntry.Key}'.");
                }

                // create new instance of Engine Element
                var engineElement = (IEngineElement)serviceProvider.GetService(engineElementType);

                // load data from entry
                engineElement.LoadFromEntity(elementEntry.Id);

                // set the element's parent module if needed
                if (elementEntry.ParentModule != null) engineElement.ParentEngineModule = modules[elementEntry.ParentModule.Id];

                engineElementsFlow.Add(engineElement);
            }
        }
    }
}