using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tasks.Model.Client;
using Tasks.Model.Domain;
using Tasks.Business.Tasks;

namespace Tasks.Web.Controller {

    [ApiController]
    [Route("task")]
    public class TaskController {
        private Mapper mapper;
        private CreateTaskBusiness createTaskBusiness;
        private UpdateTaskBusiness updateTaskBusiness;

        public TaskController(
            CreateTaskBusiness createTaskBusiness,
            UpdateTaskBusiness updateTaskBusiness,
            Mapper mapper)
        {
            this.createTaskBusiness = createTaskBusiness;
            this.updateTaskBusiness = updateTaskBusiness;
            this.mapper = mapper;
        }

        [HttpPost]
        [Consumes("application/json")]
        public TaskDto CreateTask([FromBody] TaskDto taskDto) {
            var task = mapper.Map<Task>(taskDto);
            var result = mapper.Map<TaskDto>(createTaskBusiness.Create(task));

            return result;
        }

        [HttpPut("{taskId}")]
        [Consumes("application/json")]
        public TaskDto UpdateTask([FromBody] TaskDto taskDto, string taskId) {

            taskDto.Id = int.Parse(taskId);

            var task = mapper.Map<Task>(taskDto);
            var result = mapper.Map<TaskDto>(updateTaskBusiness.Update(task));

            return result;
        }
    }

}