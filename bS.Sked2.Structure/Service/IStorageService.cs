using bS.Sked2.Structure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Service
{
    /// <summary>
    /// Abstracted file system manager. It is cross platform
    /// </summary>
    /// <seealso cref="bS.Sked2.Structure.Service.IService" />
    public interface IStorageService : IService
    {
        /// <summary>
        /// Saves the file from binary.
        /// </summary>
        /// <param name="binaryFile">The binary file.</param>
        /// <param name="path">The virtual path.</param>
        /// <returns></returns>
        bool SaveFile(byte[] binaryFile, IVirtualPath path);
        /// <summary>
        /// Saves the file from string.
        /// </summary>
        /// <param name="textFile">The text file.</param>
        /// <param name="path">The virtual path.</param>
        /// <returns></returns>
        bool SaveFile(string textFile, IVirtualPath path);
        /// <summary>
        /// Reads the binary file.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <returns></returns>
        byte[] ReadFileBinary(IVirtualPath path);
        /// <summary>
        /// Reads the text file.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <returns></returns>
        string ReadFileString(IVirtualPath path);
        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <returns></returns>
        bool DeleteFile(IVirtualPath path);
        /// <summary>
        /// Moves the file from the origin to the target virtual path.
        /// </summary>
        /// <param name="origin">The origin virtual path.</param>
        /// <param name="target">The target virtual path.</param>
        /// <returns></returns>
        bool MoveFile(IVirtualPath origin, IVirtualPath target);
        /// <summary>
        /// Copies the file from the origin to the target virtual path.
        /// </summary>
        /// <param name="origin">The origin virtual path.</param>
        /// <param name="target">The target virtual path.</param>
        /// <returns></returns>
        bool CopyFile(IVirtualPath origin, IVirtualPath target);

        /// <summary>
        /// Deletes the folder in the specified virtual path.
        /// </summary>
        /// <param name="path">The folder's virtual path.</param>
        /// <param name="deleteSubElements">if set to <c>true</c> [delete sub elements].</param>
        /// <returns></returns>
        bool DeleteFolder(IVirtualPath path, bool deleteSubElements = false);


    }
}
