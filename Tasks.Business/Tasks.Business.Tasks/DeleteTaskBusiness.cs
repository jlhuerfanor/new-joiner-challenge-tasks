using System;
using Tasks.Model.Domain;
using Tasks.Service.Persistence;
using Tasks.Service.Tasks;

namespace Tasks.Business.Tasks
{
    public class DeleteTaskBusiness
    {
        private ITaskRepositoryService taskRepositoryService;
        private ITaskQueryService taskQueryService;
        private ITransactionalService transactionalService;
        private IValidationBuilder<Task> validations;

        public DeleteTaskBusiness(ITaskRepositoryService taskRepositoryService, ITaskQueryService taskQueryService, ITransactionalService transactionalService)
        {
            this.taskRepositoryService = taskRepositoryService;
            this.taskQueryService = taskQueryService;
            this.transactionalService = transactionalService;
            
            this.validations = Validations.ComposeValidations<Task>()
                .Next(Validations.SingleValidation<Task>()
                    .When(task => task == null)
                    .Then(ValidationException.WithMessage<Task>("Task with given id could not be found.")));
        }

        public void Delete(int taskIdInt)
        {
            var task = taskQueryService.GetById(taskIdInt);

            this.validations.Evaluate(task);

            taskRepositoryService.Delete(task);
            transactionalService.Commit();
        }
    }
}