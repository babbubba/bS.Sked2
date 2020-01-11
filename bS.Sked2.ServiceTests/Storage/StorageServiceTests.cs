using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace bS.Sked2.Service.Storage.Tests
{
    [TestClass()]
    public class StorageServiceTests
    {
        private bool semaphore = true;
        private bool created = false;
        private StorageService storageService;

        [TestMethod()]
        public void StorageServiceTest()
        {
            storageService.FileSave("Ciao sono un test!", new StoragePath(@"/file_di_prova.txt"));

            Assert.IsTrue(storageService.FileExists(new StoragePath(@"/file_di_prova.txt")));


            storageService.FileCopy(new StoragePath(@"/file_di_prova.txt"), new StoragePath(@"/file_di_prova_copia.txt"));

            Assert.IsTrue(storageService.FileExists(new StoragePath(@"/file_di_prova_copia.txt")));

            storageService.FileMove(new StoragePath(@"/file_di_prova.txt"), new StoragePath(@"/file_di_prova_moved.txt"));

            Assert.IsFalse(storageService.FileExists(new StoragePath(@"/file_di_prova.txt")));

            storageService.FileDelete(new StoragePath(@"/file_di_prova_copia.txt"));

            Assert.IsFalse(storageService.FileExists(new StoragePath(@"/file_di_prova_copia.txt")));

            var val = storageService.FileReadString(new StoragePath(@"/file_di_prova_moved.txt"));

            Assert.AreEqual("Ciao sono un test!", val);

            storageService.FileDelete(new StoragePath(@"/file_di_prova_moved.txt"));

            storageService.FolderCreate(new StoragePath(@"/sub"));

            Assert.IsTrue(storageService.FolderExists(new StoragePath(@"/sub")));

            var watcher = storageService.FolderWatch(new StoragePath(@"/sub"));

            watcher.Created += Watcher_Created;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            storageService.FileSave("Ciao sono un test!", new StoragePath(@"/sub/file_di_prova_watcher.txt"));

            System.Threading.Thread.Sleep(500);

            Assert.IsTrue(created);

            storageService.FolderDelete(new StoragePath(@"/sub"), true);


        }

        private void Watcher_Created(object sender, string e)
        {
            created = true;
        }


        [TestInitialize]
        public void Init()
        {
            ILogger<StorageService> logger = Mock.Of<ILogger<StorageService>>();
            var conf = new StorageServiceConfig(@".\");
            storageService = new StorageService(logger, conf);
        }
    }
}