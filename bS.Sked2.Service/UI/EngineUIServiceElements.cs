using bS.Sked2.Model;
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
                elementEntry = (IElementEntry)engineElement.GetEmptyEntity();

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

        /// <summary>
        /// Edits the element.
        /// </summary>
        /// <param name="elementId">The element identifier.</param>
        /// <param name="elementDefinition">The element definition.</param>
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
                    var entryProperty = entry.InputProperties.FirstOrDefault(p => p.Key == inputProperty.Key);
                    var data = GetPopertyDataValue(entryProperty);
                    data.ReadFromStringValue(inputProperty.Value.Base64Decode());
                    entryProperty.Value = data.WriteToStringValue();
                }

                foreach (var outputProperties in elementDefinition.OutputProperties)
                {
                    var entryProperty = entry.OutputProperties.FirstOrDefault(p => p.Key == outputProperties.Key);
                    var data = GetPopertyDataValue(entryProperty);
                    data.ReadFromStringValue(outputProperties.Value.Base64Decode());
                    entryProperty.Value = data.WriteToStringValue();
                }

                if (elementDefinition.ParentModuleId != null && elementDefinition.ParentModuleId != entry.ParentModule?.Id)
                {
                    var parentModule = engineRepository.GetModuleById((Guid)elementDefinition.ParentModuleId);
                    entry.ParentModule = parentModule;
                }

                engineRepository.UpdateEment(entry);
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
            var result = new ElementDefinitionEditViewModel();
            var entry = engineRepository.GetElementById(elementId);
            result.Name = entry.Name;
            result.Description = entry.Description;
            result.ParentModuleId = entry.ParentModule?.Id;

            // get the Engine element
            var engineElement = GetEngineElement(entry.Key);

            // get the properties value
            // input
            var inputProperties = new List<PropertyDefinition>();
            foreach (var inputProperty in entry.InputProperties)
            {
                var engineDataValue = GetPopertyDataValue(inputProperty);
                var propertyDetail = engineElement.InputProperties.FirstOrDefault(ip => ip.Key == inputProperty.Key);

                inputProperties.Add(new PropertyDefinition
                {
                    DataType = inputProperty.DataType,
                    //Value = engineDataValue,
                    Value = engineDataValue.WriteToStringValue().Base64Encode(),
                    Key = inputProperty.Key,
                    Description = propertyDetail.Description
                });
            }
            result.InputProperties = inputProperties;

            // output
            var outputProperties = new List<PropertyDefinition>();
            foreach (var outputProperty in entry.OutputProperties)
            {
                var engineDataValue = GetPopertyDataValue(outputProperty);
                var propertyDetail = engineElement.OutputProperties.FirstOrDefault(op => op.Key == outputProperty.Key);

                outputProperties.Add(new PropertyDefinition
                {
                    DataType = outputProperty.DataType,
                    //Value = engineDataValue,
                    Value = engineDataValue.WriteToStringValue().Base64Encode(),
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
            var result = new List<IElementDefinitionPreview>();

            var elements = engineRepository.GetElements()
                .Where(e => !e.IsDeleted && e.ParentTask.Id == taskId)
                .OrderBy(t => t.Position);

            //var elements = engineRepository.GetElements()
            //   .Where(e => !e.IsDeleted && e.ParentTask.Id == taskId)
            //   .OrderBy(t => t.Position)
            //   .Select(e => new ElementDefinitionPreviewViewModel
            //   {
            //       Id = e.Id,
            //       Description = e.Description,
            //       Name = e.Name,
            //       Position = e.Position,
            //       IsEnabled = e.IsEnabled,
            //       Type = engine.GetEngineElementType(e.Key)
            //   }) ;
            foreach (var element in elements)
            {
                var type = engine.GetEngineElementType(element.Key);
                var elementViewModel = new ElementDefinitionPreviewViewModel
                {
                    Id = element.Id,
                    Description = element.Description,
                    Name = element.Name,
                    Position = element.Position,
                    IsEnabled = element.IsEnabled,
                    Type = type
                };

                if (type.Key == "ElementsLink")
                {
                    elementViewModel.PreviousId = ((ElementsLinkEntry)element).Previuous.Id;
                    elementViewModel.NextId = ((ElementsLinkEntry)element).Next.Id;
                }
                result.Add(elementViewModel);
            }
            return result;
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
            try
            {
                using (var transaction = uow.BeginTransaction())
                {
                    var element = engineRepository.GetElementById(elementId);
                    var module = engineRepository.GetModuleById(moduleId);
                    element.ParentModule = module;
                    engineRepository.UpdateEment(element);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
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