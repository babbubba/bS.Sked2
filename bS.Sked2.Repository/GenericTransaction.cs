using bS.Sked2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Repository
{
    public class GenericTransaction : IGenericTransaction
    {
        private readonly NHibernate.ITransaction _transaction;

        public GenericTransaction(NHibernate.ITransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    
    }
}
