namespace Tasks.Service.Persistence
{
    public interface ITransactionalService
    {
        void Commit();
    }
}