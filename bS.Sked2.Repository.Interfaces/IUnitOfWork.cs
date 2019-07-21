using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace bS.Sked2.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryContext Context { get; }
        void Flush();
        bool IsInActiveTransaction { get; }
        IGenericTransaction BeginTransaction();
        IGenericTransaction BeginTransaction(IsolationLevel isolationLevel);
        void TransactionalFlush();
        void TransactionalFlush(IsolationLevel isolationLevel);
    }
}
