using System.Collections.Generic;
using Tasks.Model.Domain;
using Tasks.Service.Persistence;

namespace Tasks.Service.Tasks
{
    public class EFTaskQueryService : ITaskQueryService
    {
        private TaskDbContext context;

        public EFTaskQueryService(TaskDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return context.Tasks;
        }

        public Task GetById(int id)
        {
            return context.Find<Task>(id);
        }
    }

}