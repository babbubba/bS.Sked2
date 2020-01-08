using bs.Data.Interfaces;
using bS.Sked2.Model.Repositories;
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
using System.Text;

namespace bS.Sked2.Engine.Objects
{
    public class EngineTask : BaseEngineComponent, IEngineTask
    {
        private readonly IUnitOfWork uow;
        private readonly IEngineRepository engineRepository;
        private readonly IEngine engine;
        private readonly IServiceProvider serviceProvider;
        private ITaskEntry taskEntry;

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

        public IJobEntry ParentJob { get => taskEntry.ParentJob; }

        /// <summary>
        /// Determines whether this instance [can be executed].
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance [can be executed]; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanBeExecuted()
        {
            // TODO: Add logic here if needed
            return true;
        }

        /// <summary>
        /// Loads from entity.
        /// </summary>
        /// <param name="taskId">The job identifier.</param>
        public override void LoadFromEntity(Guid taskId)
        {
            taskEntry = engineRepository.GetTaskById(taskId);
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public override void Pause()
        {
            uow.BeginTransaction();

            instance.IsPaused = true;

            uow.Commit();

            AddMessage("Task execution paused.");

        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public override void Start()
        {
            uow.BeginTransaction();

            // Create the instance ID for this element
            instance = engineRepository.CreateNewInstance();

            // Set the execution begin time
            instance.BeginTime = DateTime.Now;

            // Add current instance to entry
            taskEntry.Instances.Add(instance);

            uow.Commit();

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

                }
                catch (Exception ex)
                {
                    // unhandled exception
                    throw;
                }


            }




        }

        private void GetEngineTaskElementsFlow(List<IEngineElement> engineElementsFlow)
        {

            // fetch all entities element
            foreach (var elementEntry in taskEntry.Elements)
            {
                // finding the EngineElement to execute this entry
                var engineElementType = AssembliesExtensions.GetTypesImplementingInterface<IEngineElement>()
               .FirstOrDefault(ed => (string)ed.GetProperty("KeyConst")?.GetValue(ed) == elementEntry.Key);

                // create new instance of Engine Element
                //var engineElement = (IEngineElement)Activator.CreateInstance(engineElementType);
                var engineElement = (IEngineElement)serviceProvider.GetService(engineElementType);

                // load data from entry
                engineElement.LoadFromEntity(elementEntry.Id);

                engineElementsFlow.Add(engineElement);
            }
        }

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
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            uow.BeginTransaction();

            // It set paused value to false in case this element was paused previously
            instance.IsPaused = false;

            // Set this element finish time
            instance.EndTime = DateTime.Now;

            // Add a message to notify the element finish execution
            AddMessage("Task execution finish.");

            uow.Commit();
        }
    }
}
