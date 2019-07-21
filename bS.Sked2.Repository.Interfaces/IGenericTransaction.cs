namespace bS.Sked2.Repository.Interfaces
{
    public interface IGenericTransaction
    {
        void Commit();
        void Rollback();
    }
}