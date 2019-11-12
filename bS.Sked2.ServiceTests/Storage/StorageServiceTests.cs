using Microsoft.VisualStudio.TestTools.UnitTesting;
using bS.Sked2.Service.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;

namespace bS.Sked2.Service.Storage.Tests
{
    




    [TestClass()]
    public class StorageServiceTests
    {
        private StorageService storageService;


        [TestInitialize]
        public void Init()
        {
            ILogger<StorageService> logger = Mock.Of<ILogger<StorageService>>();
            var conf = new StorageServiceConfig(@"C:\temp");
            storageService = new StorageService(logger, conf);
        }

        [TestMethod()]
        public void FileCopyTest()
        {
            storageService.FileCopy(new StoragePath(@"/file_di_prova.txt"), new StoragePath(@"/file_di_prova_copia.txt"));
        }

        [TestMethod()]
        public void FileDeleteTest()
        {
            storageService.FileDelete(new StoragePath(@"/sub/Nuovo documento RTF.rtf"));
        }

        [TestMethod()]
        public void FileExistsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FileMoveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FileReadBinaryTest()
        {
            var binary = storageService.FileReadBinary(new StoragePath(@"/file_di_prova.txt"));

        }

        [TestMethod()]
        public void FileReadStringTest()
        {
            var text = storageService.FileReadString(new StoragePath(@"/file_di_prova.txt"));

        }

        [TestMethod()]
        public void FileSaveTest()
        {
            storageService.FileSave("Ciao sono un test!", new StoragePath(@"/file_di_prova.txt"));
        }

        [TestMethod()]
        public void FileSaveTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FolderEnumeratePathsTest()
        {
            var r = storageService.FolderEnumeratePaths(new StoragePath(@"/"), System.IO.SearchOption.AllDirectories);

        }

        [TestMethod()]
        public void FolderDeleteTest()
        {
            storageService.FolderDelete(new StoragePath(@"/sub"), true);
        }
        bool semaphore = true;
        [TestMethod()]
        public void FolderWatchTest()
        {
            var res = storageService.FolderWatch(new StoragePath("/sub"));
            res.Renamed += Res_Renamed;
            res.Created += Res_Created;
            res.Deleted += Res_Deleted;
            res.Changed += Res_Changed;
            res.IncludeSubdirectories = true;
            res.EnableRaisingEvents = true;
            while (semaphore)
            {
                //nn fa niente
            }
            

        }

        private void Res_Changed(object sender, string e)
        {
            semaphore = false;
        }

        private void Res_Deleted(object sender, string e)
        {
            semaphore = false;

        }

        private void Res_Created(object sender, string e)
        {
            semaphore = false;

        }

        private void Res_Renamed(object sender, string e)
        {
            semaphore = false;

        }
      
    }
}