using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bS.Sked2.Structure.Base.Exceptions;
using bS.Sked2.Structure.Base.FileSystem;
using bS.Sked2.Structure.Service;
using Microsoft.Extensions.Logging;
using Zio;
using Zio.FileSystems;

namespace bS.Sked2.Service.Storage
{
    /// <summary>
    /// This handle file management in a cross plattform virtual file sistem. This implement IDsiposable pattern so remeber to release resources at the end.
    /// </summary>
    /// <seealso cref="bS.Sked2.Service.Base.ServiceBase" />
    /// <seealso cref="bS.Sked2.Structure.Service.IStorageService" />
    /// <seealso cref="System.IDisposable" />
    public class StorageService : Base.ServiceBase, IStorageService, IDisposable
    {
        #region Fields
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IStorageServiceConfig config;
        /// <summary>
        /// The work space file system
        /// </summary>
        private readonly SubFileSystem workSpaceFileSystem; 
        #endregion

        #region C.tor
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="config">The configuration.</param>
        /// <exception cref="StorageException">
        /// Error initializing storage. Invalid 'RootPath' provided. - 1
        /// or
        /// Error initializing storage. {e.Message} - 2
        /// </exception>
        public StorageService(ILogger<StorageService> logger, IStorageServiceConfig config) : base(logger)
        {
            this.config = config;

            // Check if config is valid
            if (string.IsNullOrWhiteSpace(this.config.RootPath)) throw new StorageException(logger, $"Error initializing storage. Invalid 'RootPath' provided.", 1);

            // Init the virtual file system
            try
            {
                var fileSystem = new PhysicalFileSystem();
                var rootUpath = fileSystem.ConvertPathFromInternal(this.config.RootPath);
                workSpaceFileSystem = new SubFileSystem(fileSystem, rootUpath);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error initializing storage. {e.Message}", e, 2);
            }
        } 
        #endregion

        #region File Management
        /// <summary>
        /// Copies the file from the origin to the target virtual path.
        /// </summary>
        /// <param name="source">The origin virtual path.</param>
        /// <param name="target">The target virtual path.</param>
        /// <param name="overwrite"></param>
        /// <exception cref="StorageException">Error coping file from '{source.RealPath}' to '{target.RealPath}'. - 3</exception>
        public void FileCopy(IVirtualPath source, IVirtualPath target, bool overwrite = false)
        {
            try
            {
                workSpaceFileSystem.CopyFile(source.RealPath, target.RealPath, overwrite);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error coping file from '{source.RealPath}' to '{target.RealPath}'.", e, 3);
            }
        }

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <exception cref="StorageException">Error deleting file '{path.RealPath}'. - 4</exception>
        public void FileDelete(IVirtualPath path)
        {
            try
            {
                workSpaceFileSystem.DeleteFile(path.RealPath);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error deleting file '{path.RealPath}'.", e, 4);
            }
        }

        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="StorageException">Error checking if file '{path.RealPath}' exists. - 5</exception>
        public bool FileExists(IVirtualPath path)
        {
            try
            {
                return workSpaceFileSystem.FileExists(path.RealPath);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error checking if file '{path.RealPath}' exists.", e, 5);
            }
        }

        /// <summary>
        /// Files the get attributes.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool FileGetAttributes(IVirtualPath path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Files the get creation time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="StorageException">Error checking file '{path.RealPath}'. - 7</exception>
        public DateTime FileGetCreationTime(IVirtualPath path)
        {
            try
            {
                return workSpaceFileSystem.GetCreationTime(path.RealPath);

            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error checking file '{path.RealPath}'.", e, 7);
            }
        }

        /// <summary>
        /// Files the get last access time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="StorageException">Error checking file '{path.RealPath}'. - 8</exception>
        public DateTime FileGetLastAccessTime(IVirtualPath path)
        {
            try
            {
                return workSpaceFileSystem.GetLastAccessTime(path.RealPath);

            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error checking file '{path.RealPath}'.", e, 8);
            }
        }

        /// <summary>
        /// Files the get last write time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="StorageException">Error checking file '{path.RealPath}'. - 9</exception>
        public DateTime FileGetLastWriteTime(IVirtualPath path)
        {
            try
            {
                return workSpaceFileSystem.GetLastWriteTime(path.RealPath);

            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error checking file '{path.RealPath}'.", e, 9);
            }
        }

        /// <summary>
        /// Files the get lenght.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="StorageException">Error checking lenght of file '{path.RealPath}'. - 10</exception>
        public long FileGetLenght(IVirtualPath path)
        {
            try
            {
                return workSpaceFileSystem.GetFileLength(path.RealPath);

            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error checking lenght of file '{path.RealPath}'.", e, 10);
            }
        }

        /// <summary>
        /// Moves the file from the origin to the target virtual path.
        /// </summary>
        /// <param name="source">The origin virtual path.</param>
        /// <param name="target">The target virtual path.</param>
        /// <exception cref="StorageException">Error coping file from '{source.RealPath}' to '{target.RealPath}'. - 11</exception>
        public void FileMove(IVirtualPath source, IVirtualPath target)
        {
            try
            {
                workSpaceFileSystem.MoveFile(source.RealPath, target.RealPath);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error coping file from '{source.RealPath}' to '{target.RealPath}'.", e, 11);
            }
        }

        /// <summary>
        /// Reads the binary file.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <returns></returns>
        /// <exception cref="StorageException">Error reading file '{path.RealPath}'. - 12</exception>
        public byte[] FileReadBinary(IVirtualPath path)
        {
            try
            {
                return workSpaceFileSystem.ReadAllBytes(path.RealPath);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error reading file '{path.RealPath}'.", e, 12);
            }
        }

        /// <summary>
        /// Reads the text file.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <returns></returns>
        /// <exception cref="StorageException">Error reading file '{path.RealPath}'. - 13</exception>
        public string FileReadString(IVirtualPath path)
        {
            try
            {
                return workSpaceFileSystem.ReadAllText(path.RealPath);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error reading file '{path.RealPath}'.", e, 13);
            }
        }

        /// <summary>
        /// Save a bit array in a Binary File
        /// </summary>
        /// <param name="binaryContent">Content of the binary.</param>
        /// <param name="path">The path.</param>
        /// <exception cref="StorageException">Error writing file '{path.RealPath}'. - 14</exception>
        public void FileSave(byte[] binaryContent, IVirtualPath path)
        {
            try
            {
                workSpaceFileSystem.WriteAllBytes(path.RealPath, binaryContent);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error writing file '{path.RealPath}'.", e, 14);
            }
        }

        /// <summary>
        /// Save a string in a Text File
        /// </summary>
        /// <param name="textContent">Content of the text.</param>
        /// <param name="path">The path.</param>
        /// <exception cref="StorageException">Error writing file '{path.RealPath}'. - 15</exception>
        public void FileSave(string textContent, IVirtualPath path)
        {
            try
            {
                workSpaceFileSystem.WriteAllText(path.RealPath, textContent);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error writing file '{path.RealPath}'.", e, 15);
            }
        }

        /// <summary>
        /// Set the file attributes.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool FileSetAttributes(IVirtualPath path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set the file creation time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="dateTime">The date time.</param>
        /// <exception cref="StorageException">Error setting creation date to file '{path.RealPath}'. - 17</exception>
        public void FileSetCreationTime(IVirtualPath path, DateTime dateTime)
        {
            try
            {
                workSpaceFileSystem.SetCreationTime(path.RealPath, dateTime);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error setting creation date to file '{path.RealPath}'.", e, 17);
            }
        }

        /// <summary>
        /// Set the file  last access time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="dateTime">The date time.</param>
        /// <exception cref="StorageException">Error setting last access time to file '{path.RealPath}'. - 18</exception>
        public void FileSetLastAccessTime(IVirtualPath path, DateTime dateTime)
        {
            try
            {
                workSpaceFileSystem.SetLastAccessTime(path.RealPath, dateTime);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error setting last access time to file '{path.RealPath}'.", e, 18);
            }
        }

        /// <summary>
        /// Set the file  last write time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="dateTime">The date time.</param>
        /// <exception cref="StorageException">Error setting last write time to file '{path.RealPath}'. - 19</exception>
        public void FileSetLastWriteTime(IVirtualPath path, DateTime dateTime)
        {
            try
            {
                workSpaceFileSystem.SetLastWriteTime(path.RealPath, dateTime);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error setting last write time to file '{path.RealPath}'.", e, 19);
            }
        }
        #endregion File Management

        #region Folder Management
        /// <summary>
        /// Folders the create.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="StorageException">Error creating directory '{path.RealPath}'. - 20</exception>
        public void FolderCreate(IVirtualPath path)
        {
            try
            {
                workSpaceFileSystem.CreateDirectory(path.RealPath);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error creating directory '{path.RealPath}'.", e, 20);
            }
        }

        /// <summary>
        /// Folders the delete.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="deleteSubElements">if set to <c>true</c> [delete sub elements].</param>
        /// <exception cref="StorageException">Error deleting folder '{path.RealPath}'. - 21</exception>
        public void FolderDelete(IVirtualPath path, bool deleteSubElements = false)
        {
            try
            {
                workSpaceFileSystem.DeleteDirectory(path.RealPath, deleteSubElements);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error deleting folder '{path.RealPath}'.", e, 21);
            }
        }

        /// <summary>
        /// Folders the enumerate paths.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        /// <exception cref="StorageException">Error checking folder '{path.RealPath}'. - 22</exception>
        public IVirtualPath[] FolderEnumeratePaths(IVirtualPath path, System.IO.SearchOption searchOption, string searchPattern = "*")
        {
            try
            {
                return workSpaceFileSystem.EnumeratePaths(path.RealPath, searchPattern, searchOption).Select(p => new StoragePath(p.ToAbsolute().ToString())).ToArray();
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error checking folder '{path.RealPath}'.", e, 22);
            }
        }

        /// <summary>
        /// Folders the exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="StorageException">Error checking folder '{path.RealPath}'. - 23</exception>
        public bool FolderExists(IVirtualPath path)
        {
            try
            {
                return workSpaceFileSystem.DirectoryExists(path.RealPath);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error checking folder '{path.RealPath}'.", e, 23);
            }
        }

        /// <summary>
        /// Folders the move.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <exception cref="StorageException">Error moving folder from '{source.RealPath}' to '{target.RealPath}'. - 24</exception>
        public void FolderMove(IVirtualPath source, IVirtualPath target, bool overwrite = false)
        {
            try
            {
                workSpaceFileSystem.MoveDirectory(source.RealPath, target.RealPath);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error moving folder from '{source.RealPath}' to '{target.RealPath}'.", e, 24);
            }
        }

        /// <summary>
        /// Watch the folder changes. You have to register the events that the return value implements for handle folder's changes.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="StorageException">Error watching folder '{path.RealPath}'. - 25</exception>
        public IVirtualPathWatch FolderWatch(IVirtualPath path)
        {
            try
            {
                var res = workSpaceFileSystem.Watch(path.RealPath);
                return new VirtualPathWatch(res);
            }
            catch (Exception e)
            {
                throw new StorageException(logger, $"Error watching folder '{path.RealPath}'.", e, 25);
            }
        }
        #endregion Folder Management

        #region IDisposable Support
        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false; // Per rilevare chiamate ridondanti

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    workSpaceFileSystem.Dispose();
                }

                // TODO: liberare risorse non gestite (oggetti non gestiti) ed eseguire sotto l'override di un finalizzatore.
                // TODO: impostare campi di grandi dimensioni su Null.


                disposedValue = true;
            }
        }

        // TODO: eseguire l'override di un finalizzatore solo se Dispose(bool disposing) include il codice per liberare risorse non gestite.
        // ~StorageService()
        // {
        //   // Non modificare questo codice. Inserire il codice di pulizia in Dispose(bool disposing) sopra.
        //   Dispose(false);
        // }

        // Questo codice viene aggiunto per implementare in modo corretto il criterio Disposable.
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Non modificare questo codice. Inserire il codice di pulizia in Dispose(bool disposing) sopra.
            Dispose(true);
            // TODO: rimuovere il commento dalla riga seguente se è stato eseguito l'override del finalizzatore.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
