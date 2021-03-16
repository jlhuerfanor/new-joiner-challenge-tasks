using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tasks.Model.Client;
using Tasks.Model.Domain;
using Tasks.Business.Tasks;
using System.Collections.Generic;

namespace Tasks.Web.Controller {

    [ApiController]
    [Route("task")]
    public class TaskController {
        private Mapper mapper;
        private CreateTaskBusiness createTaskBusiness;
        private UpdateTaskBusiness updateTaskBusiness;
        private DeleteTaskBusiness deleteTaskBusiness;
        private QueryTaskBusiness queryTaskBusiness;

        public TaskController(
            CreateTaskBusiness createTaskBusiness,
            UpdateTaskBusiness updateTaskBusiness,
            DeleteTaskBusiness deleteTaskBusiness,
            QueryTaskBusiness queryTaskBusiness,
            Mapper mapper)
        {
            this.createTaskBusiness = createTaskBusiness;
            this.updateTaskBusiness = updateTaskBusiness;
            this.deleteTaskBusiness = deleteTaskBusiness;
            this.queryTaskBusiness = queryTaskBusiness;
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
        [HttpDelete("{taskId}")]
        [Consumes("application/json")]
        public void DeletesTask(string taskId) {
            var taskIdInt = int.Parse(taskId);

            this.deleteTaskBusiness.Delete(taskIdInt);            
        }
        [HttpGet("{taskId}")]
        [Consumes("application/json")]
        public TaskDto GetTaskDetails(string taskId) {
            var taskIdInt = int.Parse(taskId);
            var task = queryTaskBusiness.GetById(taskIdInt);

            return mapper.Map<TaskDto>(task);
        }
        [HttpGet]
        public IList<int> GetTaskIds() {
            return queryTaskBusiness.GetTaskIds();
        }
    }

}