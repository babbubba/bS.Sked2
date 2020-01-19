using System;

namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Module's interface. Amodule rapresents an extensions that contains one or more elements.
    /// </summary>
    public interface IEngineModule : IEngineFlowComponent
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; }

        bool IsInit { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Init();

        //void LoadFromEntity(Guid id);

        IEngineElementProperty[] InputProperties { get; }
        IEngineElementProperty[] OutputProperties { get; }
    }
}