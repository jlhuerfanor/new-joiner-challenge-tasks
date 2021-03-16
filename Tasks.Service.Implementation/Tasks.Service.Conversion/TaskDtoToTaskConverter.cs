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

            result.Name = source.Name != null ? source.Name : result.Name;
            result.Description = source.Description != null ? source.Description : result.Description;
            result.Completed = source.Completed.HasValue ?  source.Completed.Value : result.Completed;
            result.EstimatedRequiredHours = source.EstimatedRequiredHours.HasValue ? source.EstimatedRequiredHours.Value : result.EstimatedRequiredHours;
            result.Stack = source.Stack != null ? source.Stack : result.Stack;
            result.MinimumRoles = source.MinimumRoles != null ? new List<string>(source.MinimumRoles) : result.MinimumRoles;
            result.AssignedIdNumber = source.AssignedIdNumber.HasValue ? source.AssignedIdNumber.Value : result.AssignedIdNumber;
            result.ParentTask = source.ParentTaskId.HasValue? queryService.GetById(source.ParentTaskId.Value) : result.ParentTask;

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