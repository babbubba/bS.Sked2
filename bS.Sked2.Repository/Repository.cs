using bS.Sked2.Model.Interfaces;
using bS.Sked2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bS.Sked2.Repository
{
    public class Repository<TBase> : IRepository<TBase> where TBase : class, IPersisterEntity
    {
        //private static ILog log = LogManager.GetLogger<Repository<TBase>>();

        private readonly IUnitOfWork _mainUnitOfWork;

        public Repository(IUnitOfWork mainUnitOfWork)
        {
            _mainUnitOfWork = mainUnitOfWork;
            //log.Trace($"Repository created for the Unit Of Work '{mainUnitOfWork.GetHashCode()}'.");
        }

        #region IRepository<TBase> Members

        public IQueryable<TImpl> GetQuery<TImpl>(IUnitOfWork uow) where TImpl : class, TBase
        {
            return uow.Context.CreateObjectSet<TImpl>();
        }

        public IQueryable<TImpl> GetQuery<TImpl>() where TImpl : class, TBase
        {
            return GetQuery<TImpl>(_mainUnitOfWork);
        }

        public void Add<TImpl>(TImpl item, IUnitOfWork uow) where TImpl : class, TBase
        {
            uow.Context.CreateObjectSet<TImpl>().Add(item);
        }

        public void Add<TImpl>(TImpl item) where TImpl : class, TBase
        {
            Add<TImpl>(item, _mainUnitOfWork);
            FlushIfMainUnitOfWorkIsNotInTransaction();
        }

        public void Update<TImpl>(TImpl item, IUnitOfWork uow) where TImpl : class, TBase
        {
            uow.Context.CreateObjectSet<TImpl>().Update(item);
        }

        public void Update<TImpl>(TImpl item) where TImpl : class, TBase
        {
            Update<TImpl>(item, _mainUnitOfWork);
            FlushIfMainUnitOfWorkIsNotInTransaction();
        }

        public void Delete<TImpl>(TImpl item, IUnitOfWork uow) where TImpl : class, TBase
        {
            uow.Context.CreateObjectSet<TImpl>().Delete(item);
        }

        public void Delete<TImpl>(TImpl item) where TImpl : class, TBase
        {
            Delete<TImpl>(item, _mainUnitOfWork);
            FlushIfMainUnitOfWorkIsNotInTransaction();
        }

        public bool IsInActiveTransaction
        {
            get
            {
                return _mainUnitOfWork.IsInActiveTransaction;
            }
        }

        public IGenericTransaction BeginTransaction(IUnitOfWork uow)
        {
            return uow.BeginTransaction();
        }

        public IGenericTransaction BeginTransaction()
        {
            return BeginTransaction(_mainUnitOfWork);
        }

        public IGenericTransaction BeginTransaction(IsolationLevel isolationLevel, IUnitOfWork uow)
        {
            return uow.BeginTransaction(isolationLevel);
        }

        public IGenericTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return BeginTransaction(isolationLevel, _mainUnitOfWork);
        }

        #endregion

        #region Private Methods

        private void FlushIfMainUnitOfWorkIsNotInTransaction()
        {
            if (!_mainUnitOfWork.IsInActiveTransaction) _mainUnitOfWork.Flush();
        }

        #endregion
    }
}
