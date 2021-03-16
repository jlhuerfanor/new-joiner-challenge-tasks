using System.Collections.Generic;
using Tasks.Model.Domain;

namespace Tasks.Service.Tasks
{
    public interface ITaskQueryService
    {
        Task GetById(int id);
        IEnumerable<Task> GetAllTasks();
    }
    
}