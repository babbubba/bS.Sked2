using bS.Sked2.Structure.Base.FileSystem;
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
        void FileSave(byte[] binaryFile, IVirtualPath path);
        /// <summary>
        /// Saves the file from string.
        /// </summary>
        /// <param name="textFile">The text file.</param>
        /// <param name="path">The virtual path.</param>
        /// <returns></returns>
        void FileSave(string textFile, IVirtualPath path);
        /// <summary>
        /// Reads the binary file.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <returns></returns>
        byte[] FileReadBinary(IVirtualPath path);
        /// <summary>
        /// Reads the text file.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <returns></returns>
        string FileReadString(IVirtualPath path);
        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <returns></returns>
        void FileDelete(IVirtualPath path);
        /// <summary>
        /// Copies the file from the origin to the target virtual path.
        /// </summary>
        /// <param name="source">The origin virtual path.</param>
        /// <param name="target">The target virtual path.</param>
        /// <returns></returns>
        void FileCopy(IVirtualPath source, IVirtualPath target, bool overwrite = false);
        /// <summary>
        /// Moves the file from the origin to the target virtual path.
        /// </summary>
        /// <param name="source">The origin virtual path.</param>
        /// <param name="target">The target virtual path.</param>
        /// <returns></returns>
        void FileMove(IVirtualPath source, IVirtualPath target);
        long FileGetLenght(IVirtualPath path);
        bool FileExists(IVirtualPath path);
        bool FileGetAttributes(IVirtualPath path);
        bool FileSetAttributes(IVirtualPath path);
        DateTime FileGetCreationTime(IVirtualPath path);
        void FileSetCreationTime(IVirtualPath path, DateTime dateTime);
        DateTime FileGetLastAccessTime(IVirtualPath path);
        void FileSetLastAccessTime(IVirtualPath path, DateTime dateTime);
        DateTime FileGetLastWriteTime(IVirtualPath path);
        void FileSetLastWriteTime(IVirtualPath path, DateTime dateTime);


        void FolderCreate(IVirtualPath path);
        bool FolderExists(IVirtualPath path);
        void FolderDelete(IVirtualPath path, bool deleteSubElements = false);
        //void FolderCopy(IVirtualPath source, IVirtualPath target, bool overwrite = false);
        void FolderMove(IVirtualPath source, IVirtualPath target, bool overwrite = false);
        IVirtualPath[] FolderEnumeratePaths(IVirtualPath path, System.IO.SearchOption searchOption, string searchPattern = "*");
        IVirtualPathWatch FolderWatch(IVirtualPath path);

    }
}
