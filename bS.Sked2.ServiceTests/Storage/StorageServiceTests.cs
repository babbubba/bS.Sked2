using bS.Sked2.Model.Service;
using bS.Sked2.Structure.Engine.Data.Types;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace bS.Sked2.Service.Storage.Tests
{
    [TestClass()]
    public class StorageServiceTests
    {
        private bool created = false;
        private StorageService storageService;

        [TestMethod()]
        public void StorageServiceTest()
        {
            storageService.FileSave("Ciao sono un test!", new VirtualPath(@"/file_di_prova.txt"));

            Assert.IsTrue(storageService.FileExists(new VirtualPath(@"/file_di_prova.txt")));

            storageService.FileCopy(new VirtualPath(@"/file_di_prova.txt"), new VirtualPath(@"/file_di_prova_copia.txt"));

            Assert.IsTrue(storageService.FileExists(new VirtualPath(@"/file_di_prova_copia.txt")));

            storageService.FileMove(new VirtualPath(@"/file_di_prova.txt"), new VirtualPath(@"/file_di_prova_moved.txt"));

            Assert.IsFalse(storageService.FileExists(new VirtualPath(@"/file_di_prova.txt")));

            storageService.FileDelete(new VirtualPath(@"/file_di_prova_copia.txt"));

            Assert.IsFalse(storageService.FileExists(new VirtualPath(@"/file_di_prova_copia.txt")));

            var val = storageService.FileReadText(new VirtualPath(@"/file_di_prova_moved.txt"));

            Assert.AreEqual("Ciao sono un test!", val);

            storageService.FileDelete(new VirtualPath(@"/file_di_prova_moved.txt"));

            storageService.FolderCreate(new VirtualPath(@"/sub"));

            Assert.IsTrue(storageService.FolderExists(new VirtualPath(@"/sub")));

            var watcher = storageService.FolderWatch(new VirtualPath(@"/sub"));

            watcher.Created += Watcher_Created;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            storageService.FileSave("Ciao sono un test!", new VirtualPath(@"/sub/file_di_prova_watcher.txt"));

            System.Threading.Thread.Sleep(500);

            Assert.IsTrue(created);

            storageService.FolderDelete(new VirtualPath(@"/sub"), true);
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