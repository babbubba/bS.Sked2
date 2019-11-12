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
    public class StorageService : Base.ServiceBase, IStorageService
    {
        private readonly IStorageServiceConfig config;
        //private readonly PhysicalFileSystem fileSystem;
        private readonly SubFileSystem workSpaceFileSystem;

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

        public bool FileGetAttributes(IVirtualPath path)
        {
            throw new NotImplementedException();
        }

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

        public bool FileSetAttributes(IVirtualPath path)
        {
            throw new NotImplementedException();
        }

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
    }
}
