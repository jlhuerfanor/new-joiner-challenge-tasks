using Tasks.Model.Domain;

namespace Tasks.Service.Tasks
{
    public interface ITaskRepositoryService
    {
        Task Persist(Task task);
    }
}