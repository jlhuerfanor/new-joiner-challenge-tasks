using System;
using Tasks.Model.Domain;
using Tasks.Service.Joiner;
using Tasks.Service.Persistence;
using Tasks.Service.Tasks;

namespace Tasks.Business.Tasks {

    public class CreateTaskBusiness
    {
        private ITaskRepositoryService taskRepositoryService;
        private IJoinerQueryService joinerQueryService;
        private ITransactionalService transactionalService;

        private IValidationBuilder<Task> validation;

        public CreateTaskBusiness(ITaskRepositoryService taskRepositoryService, IJoinerQueryService joinerQueryService, ITransactionalService transactionalService)
        {
            this.taskRepositoryService = taskRepositoryService;
            this.transactionalService = transactionalService;
            this.joinerQueryService = joinerQueryService;

            this.validation = Validations.ComposeValidations<Task>()
                .Next(Validations.SingleValidation<Task>()
                    .When((task) => String.IsNullOrWhiteSpace(task.Name))
                    .Then(ValidationException.WithMessage<Task>("Task name must not be null.")))
                .Next(Validations.SingleValidation<Task>()
                    .When((task) => task.EstimatedRequiredHours <= 0)
                    .Then(ValidationException.WithMessage<Task>("Estimated required hours is not valid. Must be greater than zero.")))
                .Next(Validations.SingleValidation<Task>()
                    .When((task) => String.IsNullOrWhiteSpace(task.Stack))
                    .Then(ValidationException.WithMessage<Task>("Stack must not be null.")))
                .Next(Validations.SingleValidation<Task>()
                    .When((task) => task.AssignedIdNumber <= 0)
                    .Then(ValidationException.WithMessage<Task>("Assigned Id Number is not valid. Must be greater than zero.")))
                .Next(Validations.SingleValidation<Task>()
                    .When((task) => this.joinerQueryService.GetProfile(task.AssignedIdNumber) == null)
                    .Then(ValidationException.WithMessage<Task>("Assigned Id Number does not exist.")));
        }

        public Task Create(Task task)
        {            
            this.validation.Evaluate(task);
            var result = this.taskRepositoryService.Persist(task);   
            transactionalService.Commit();
            
            return result;
        }
    }
}