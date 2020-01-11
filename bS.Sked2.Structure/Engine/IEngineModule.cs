namespace bS.Sked2.Structure.Engine
{
    /// <summary>
    /// Module's interface. Amodule rapresents an extensions that contains one or more elements.
    /// </summary>
    public interface IEngineModule
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Init();
    }
}