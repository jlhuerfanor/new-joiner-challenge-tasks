using System;
using System.Collections.Generic;
using AutoMapper;
using Tasks.Model.Client;
using Tasks.Model.Domain;
using Tasks.Service.Tasks;

namespace Tasks.Service.Conversion
{
    public class TaskDtoToTaskConverter : ITypeConverter<TaskDto, Task>, ITypeConverter<Task, TaskDto>
    {
        private ITaskQueryService queryService;

        public TaskDtoToTaskConverter(ITaskQueryService queryService)
        {
            this.queryService = queryService;
        }

        public Task Convert(TaskDto source, Task destination, ResolutionContext context)
        {
            var result = default(Task);

            if(source.Id.HasValue) {
                result = queryService.GetById(source.Id.Value);
            } else {
                result = new Task();
            }

            result.Name = source.Name;
            result.Description = source.Description;
            result.Completed = source.Completed;
            result.EstimatedRequiredHours = source.EstimatedRequiredHours;
            result.Stack = source.Stack;
            result.MinimumRoles = source.MinimumRoles != null ? new List<string>(source.MinimumRoles) : null;
            result.AssignedIdNumber = source.AssignedIdNumber;
            result.ParentTask = source.ParentTaskId.HasValue? queryService.GetById(source.ParentTaskId.Value) : null;

            return result;
        }

        public TaskDto Convert(Task source, TaskDto destination, ResolutionContext context)
        {
            var result = new TaskDto();

            result.Id =  source.Id;
            result.Name = source.Name;
            result.Description = source.Description;
            result.Completed = source.Completed;
            result.EstimatedRequiredHours = source.EstimatedRequiredHours;
            result.Stack = source.Stack;
            result.MinimumRoles = source.MinimumRoles != null ? new List<string>(source.MinimumRoles) : null;
            result.AssignedIdNumber = source.AssignedIdNumber;
            result.ParentTaskId = source.ParentTask?.Id;

            return result;
        }
    }

}