using System;
using System.Linq;
using System.Collections.Generic;
using Tasks.Model.Domain;
using Tasks.Service.Tasks;

namespace Tasks.Business.Tasks
{
    public class QueryTaskBusiness
    {
        private ITaskQueryService taskQueryService;

        public QueryTaskBusiness(ITaskQueryService taskQueryService)
        {
            this.taskQueryService = taskQueryService;
        }

        public Task GetById(int taskId)
        {
            return taskQueryService.GetById(taskId);
        }

        public IList<int> GetTaskIds()
        {
            return taskQueryService.GetAllTasks()
                .Select(task => task.Id)
                .ToList();
        }
    }
}