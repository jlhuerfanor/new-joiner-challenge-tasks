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
        
        public TaskController(
            CreateTaskBusiness createTaskBusiness,
            Mapper mapper
        ) {
            this.createTaskBusiness = createTaskBusiness;
            this.mapper = mapper;
        }

        [HttpPost]
        [Consumes("application/json")]
        public TaskDto CreateTask([FromBody] TaskDto taskDto) {
            var task = mapper.Map<Task>(taskDto);
            var result = mapper.Map<TaskDto>(createTaskBusiness.Create(task));

            return result;
        }
    }

}