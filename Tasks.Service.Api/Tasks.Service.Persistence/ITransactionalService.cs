namespace Tasks.Service.Persistence
{
    public interface ITransactionalService
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}