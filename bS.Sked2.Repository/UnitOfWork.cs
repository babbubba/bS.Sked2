using bS.Sked2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace bS.Sked2.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //private static ILog log = LogManager.GetLogger<UnitOfWork>();

        private readonly IRepositoryContext objectContext;

        public UnitOfWork(
            IRepositoryContext objectContext)
        {
            this.objectContext = objectContext;
            //log.Trace($"Unit Of Work for the context '{objectContext.GetHashCode()}' created.");
        }

        #region IUnitOfWork Members

        public IRepositoryContext Context
        {
            get
            {
                return objectContext;
            }
        }

        public void Flush()
        {
            objectContext.Flush();
            //log.Trace($"Unit Of Work has flushed the context '{objectContext.GetHashCode()}'.");
        }

        public bool IsInActiveTransaction
        {
            get
            {
                return objectContext.IsInActiveTransaction;
            }
        }

        public IGenericTransaction BeginTransaction()
        {
            return objectContext.BeginTransaction();
        }

        public IGenericTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return objectContext.BeginTransaction(isolationLevel);
        }

        public void TransactionalFlush()
        {
            objectContext.TransactionalFlush();
        }

        public void TransactionalFlush(IsolationLevel isolationLevel)
        {
            objectContext.TransactionalFlush(isolationLevel);
        }

        #endregion IUnitOfWork Members

        #region IDisposable Members

        public void Dispose()
        {
            if (objectContext != null)
            {
                objectContext.Dispose();
                //log.Trace($"Unit Of Work has disposed the context '{h}'.");
            }
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}
