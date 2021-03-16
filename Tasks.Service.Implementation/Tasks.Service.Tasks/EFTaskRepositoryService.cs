using Tasks.Model.Domain;
using Tasks.Service.Persistence;

namespace Tasks.Service.Tasks
{
    public class EFTaskRepositoryService : ITaskRepositoryService
    {

        private TaskDbContext context;

        public EFTaskRepositoryService(TaskDbContext context)
        {
            this.context = context;
        }

        public void Delete(Task task)
        {
            this.context.Remove(task);
        }

        public Task Persist(Task task)
        {
            return context.Add<Task>(task).Entity;
        }
    }

}