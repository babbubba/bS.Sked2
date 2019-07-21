using bS.Sked2.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace bS.Sked2.Repository.Interfaces
{
    public interface IRepositoryContext : IDisposable
    {
        IObjectSet<T> CreateObjectSet<T>() where T : class, IPersisterEntity;

        void Flush();

        bool IsInActiveTransaction { get; }
        IGenericTransaction BeginTransaction();
        IGenericTransaction BeginTransaction(IsolationLevel isolationLevel);
        void TransactionalFlush();
        void TransactionalFlush(IsolationLevel isolationLevel);
    }
}
