using bs.Data;
using bs.Data.Interfaces;
using bs.Data.Interfaces.BaseEntities;
using bS.Sked2.Extensions.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace bS.Sked2.EntityTests
{
    [TestClass]
    public class UnitTest1
    {
        private static IUnitOfWork CreateUnitOfWork_Sqlite()
        {
            var dbContext = new DbContext
            {
                ConnectionString = "Data Source=.\\bs.Data.Test.db;Version=3;BinaryGuid=False;",
                DatabaseEngineType = "sqlite",
                Create = true,
                Update = true,
                LookForEntitiesDllInCurrentDirectoryToo = true,
                EntitiesFileNameScannerPatterns= new string[] { "bS.Sked2.Extensions.*.dll", "bS.Sked2.Model.dll" }
            };
            var uOW = new UnitOfWork(dbContext);
            return uOW;
        }

        [TestMethod]
        public void TestMethod1()
        {
            IUnitOfWork uOW = CreateUnitOfWork_Sqlite(); 
            var repository = new TestRepository(uOW); 

            #region Create Entity
            uOW.BeginTransaction();
            var entityToCreate = new FlatFileReaderEntry
            {
                ColumnDelimiter = "<char>;</char>",
                FirstRowHasHeader = "<boolean>true</boolean>",
                SourceFilePath = @"<string>c:\temp</string>",
                LimitToRows = "<int>0</int>",
                SkipStartingDataRows = "<int>0</int>"
            };
            repository.Create(entityToCreate);
            uOW.Commit();
            #endregion
        }
    }

    public class TestRepository : Repository
    {
        public TestRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        internal new void Create<T>(T entityToCreate) where T : IPersistentEntity
        {
            base.Create<T>(entityToCreate);
        }

        internal new T GetById<T>(Guid id) where T : IPersistentEntity
        {
            return base.GetById<T>(id);
        }

        internal new void Update<T>(T entity) where T : IPersistentEntity
        {
            base.Update<T>(entity);
        }

        internal new void Delete<T>(Guid id) where T : IPersistentEntity
        {
            base.Delete<T>(id);
        }
    }


}
