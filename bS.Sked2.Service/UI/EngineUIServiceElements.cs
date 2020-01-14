using bS.Sked2.Model.UI;
using bS.Sked2.Service.Base;
using bS.Sked2.Shared;
using bS.Sked2.Structure.Base.Exceptions;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Engine.UI;
using bS.Sked2.Structure.Models;
using bS.Sked2.Structure.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bS.Sked2.Service.UI
{
    public partial class EngineUIService : ServiceBase, IEngineUIService
    {
        /// <summary>
        /// Creates the new element.
        /// </summary>
        /// <param name="elementDefinition">The element definition.</param>
        /// <returns></returns>
        /// <exception cref="EngineException">
        /// Error creating new element. The parent task not exists.
        /// or
        /// Error creating new element. The element type not exists.
        /// </exception>
        public Guid CreateNewElement(IElementDefinitionCreate elementDefinition)
        {
            IElementEntry elementEntry = null;
            using (var transaction = uow.BeginTransaction())
            {
                // Load parent task
                var taskEntry = engineRepository.GetTaskById(elementDefinition.ParentTaskId);

                // Check if parent task exists
                if (taskEntry == null) throw new EngineException(logger, "Error creating new element. The parent task not exists.");

                // get the engine element
                IEngineElement engineElement = GetEngineElement(elementDefinition.ElementTypeKey);

                // Get the right entry model for this engine element type
                elementEntry = engineElement.GetEmptyEntity();

                // Populate the entry element whit data provided by user
                // elementEntry.ParentTask = taskEntry;
                elementEntry.Name = elementDefinition.Name;
                elementEntry.Description = elementDefinition.Description;
                elementEntry.IsEnabled = true;

                // create the element in db
                engineRepository.CreateElement(elementEntry);
            }

            // Add new element to parent task
            AddElementToTask(elementDefinition.ParentTaskId, elementEntry.Id);

            return elementEntry.Id;
        }

        public void DeleteElement(Guid elementId)
        {
            throw new NotImplementedException();
        }

        public void EditElement(Guid elementId, IElementDefinitionEdit elementDefinition)
        {
            IElementEntry entry = null;
            using (var transaction = uow.BeginTransaction())
            {
                entry = engineRepository.GetElementById(elementId);
                entry.Name = elementDefinition.Name;
                entry.Description = elementDefinition.Description;


                foreach (var inputProperty in elementDefinition.InputProperties)
                {
                    entry.InputProperties.FirstOrDefault(p => p.Key == inputProperty.Key).Value = ((IEngineData)inputProperty.Value).WriteToStringValue();
                }
                //foreach (var inputProperty in entry.InputProperties)
                //{
                //    inputProperty.Value = ((IEngineData)elementDefinition.InputProperties
                //        .FirstOrDefault(p => p.Key == inputProperty.Key).Value)
                //        .WriteToStringValue();
                //}

                //foreach (var outputProperty in entry.OutputProperties)
                //{
                //    outputProperty.Value = ((IEngineData)elementDefinition.OutputProperties
                //        .FirstOrDefault(p => p.Key == outputProperty.Key).Value)
                //        .WriteToStringValue();
                //}
                foreach (var outputProperties in elementDefinition.OutputProperties)
                {
                    entry.OutputProperties.FirstOrDefault(p => p.Key == outputProperties.Key).Value = ((IEngineData)outputProperties.Value).WriteToStringValue();
                }
            }

            if (elementDefinition.ParentModuleId != null && elementDefinition.ParentModuleId != entry.ParentModule.Id)
            {
                SetElementModule(elementId, (Guid)elementDefinition.ParentModuleId);
            }
        }

        /// <summary>
        /// Gets the creation view model for the element.
        /// </summary>
        /// <returns></returns>
        public IElementDefinitionCreate GetCreateElement()
        {
            return new ElementDefinitionCreateViewModel
            {
                Name = "New Element",
                Description = "New Element description",
                ElementTypesList = GetElementTypes()
            };
        }

        public IElementDefinitionEdit GetEditElement(Guid elementId)
        {
            var result = new ElementDefinitionEdit();
            var entry = engineRepository.GetElementById(elementId);
            result.Name = entry.Name;
            result.Description = entry.Description;
            result.ParentModuleId = entry.ParentModule?.Id;

            // get the Engine element
            var engineElement = GetEngineElement(entry.Key);

            // get the properties value
            // input
            var inputProperties = new List<ElementPropertyDefinition>();
            foreach (var inputProperty in entry.InputProperties)
            {
                var engineDataValue = GetPopertyDataValue(inputProperty);
                var propertyDetail = engineElement.InputProperties.FirstOrDefault(ip => ip.Key == inputProperty.Key);

                inputProperties.Add(new ElementPropertyDefinition
                {
                    DataType = inputProperty.DataType,
                    Value = engineDataValue,
                    Key = inputProperty.Key,
                    Description = propertyDetail.Description
                });
            }
            result.InputProperties = inputProperties;

            // output
            var outputProperties = new List<ElementPropertyDefinition>();
            foreach (var outputProperty in entry.OutputProperties)
            {
                var engineDataValue = GetPopertyDataValue(outputProperty);
                var propertyDetail = engineElement.OutputProperties.FirstOrDefault(op => op.Key == outputProperty.Key);

                outputProperties.Add(new ElementPropertyDefinition
                {
                    DataType = outputProperty.DataType,
                    Value = engineDataValue,
                    Key = outputProperty.Key,
                    Description = propertyDetail.Description
                });
            }
            result.OutputProperties = outputProperties;

            return result;
        }

        public IEnumerable<IElementDefinitionDetail> GetElements(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IElementDefinitionPreview> GetElementsPreview(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModuleDefinitionDetail> GetModulesForElement(IElementType elementType)
        {
            throw new NotImplementedException();
        }

        public void MoveElementDown(Guid elementId)
        {
            throw new NotImplementedException();
        }

        public void MoveElementUp(Guid elementId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveModuleFromElement(Guid elementId, Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public bool SetElementModule(Guid elementId, Guid moduleId)
        {
            throw new NotImplementedException();
        }

        private static IEngineData GetPopertyDataValue(IElementPropertyEntry inputProperty)
        {
            var engineDataType = AssembliesExtensions.GetTypesImplementingInterface<IEngineData>() //current loaded asemblies
                .FirstOrDefault(ed => (DataType)ed.GetProperty("DataTypeConst").GetValue(ed) == inputProperty.DataType);

            var engineDataValue = (IEngineData)Activator.CreateInstance(engineDataType);

            engineDataValue.ReadFromStringValue(inputProperty.Value);
            return engineDataValue;
        }

        /// <summary>
        /// Gets the element types from extensions folder assemblies.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IElementType> GetElementTypes()
        {
            return AssembliesExtensions.GetTypesImplementingInterface<IEngineElement>(new string[] { configuration.ExtensionsFolder }, true)
                      .Select(ed => new ElementTypeViewModel
                      {
                          Key = (string)ed.GetProperty("KeyConst")?.GetValue(ed),
                          Name = (string)ed.GetProperty("Name")?.GetValue(ed),
                          Description = (string)ed.GetProperty("Description")?.GetValue(ed)
                      });
        }

        /// <summary>
        /// Gets the engine element.
        /// </summary>
        /// <param name="elementTypeKey">The element type key.</param>
        /// <returns></returns>
        /// <exception cref="EngineException">Error. The element type not exists.</exception>
        private IEngineElement GetEngineElement(string elementTypeKey)
        {
            var engineElementType = AssembliesExtensions.GetTypesImplementingInterface<IEngineElement>(new string[] { configuration.ExtensionsFolder }, true)
                .FirstOrDefault(ed => (string)ed.GetProperty("KeyConst")?.GetValue(ed) == elementTypeKey);

            // Check if the element type exists
            if (engineElementType == null) throw new EngineException(logger, "Error. The element type not exists.");

            // Load the engine element using engine service provider
            var engineElement = (IEngineElement)engine.ServiceProvider.GetService(engineElementType);
            return engineElement;
        }
    }
}