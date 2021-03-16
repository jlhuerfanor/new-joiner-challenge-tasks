using System;
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
    }
}