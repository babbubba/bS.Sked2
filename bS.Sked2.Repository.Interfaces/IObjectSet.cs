using bS.Sked2.Model.Interfaces;
using System.Linq;

namespace bS.Sked2.Repository.Interfaces
{
    public interface IObjectSet<T> : IQueryable<T> where T : class, IPersisterEntity
    {
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}