using System;

namespace bS.Sked2.Structure.Base.FileSystem
{
    /// <summary>
    /// It rapresent the result value for a virtual file/folder watcher.
    /// </summary>
    public interface IVirtualPathWatch
    {
        /// <summary>
        /// Occurs when [changed].
        /// </summary>
        event EventHandler<string> Changed;

        /// <summary>
        /// Occurs when [created].
        /// </summary>
        event EventHandler<string> Created;

        /// <summary>
        /// Occurs when [deleted].
        /// </summary>
        event EventHandler<string> Deleted;

        /// <summary>
        /// Occurs when [renamed].
        /// </summary>
        event EventHandler<string> Renamed;

        /// <summary>
        /// Sets a value indicating whether [enable raising events].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable raising events]; otherwise, <c>false</c>.
        /// </value>
        bool EnableRaisingEvents { set; }

        /// <summary>
        /// Sets a value indicating whether [include subdirectories].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include subdirectories]; otherwise, <c>false</c>.
        /// </value>
        bool IncludeSubdirectories { set; }
    }
}