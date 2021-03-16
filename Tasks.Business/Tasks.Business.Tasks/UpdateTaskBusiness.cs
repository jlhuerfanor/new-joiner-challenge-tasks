using System;
using Tasks.Model.Domain;
using Tasks.Service.Joiner;
using Tasks.Service.Persistence;

namespace Tasks.Business.Tasks
{
    public class UpdateTaskBusiness
    {
        private IJoinerQueryService joinerQueryService;
        private ITransactionalService transactionalService;
        private IValidationBuilder<Task> validation;

        public UpdateTaskBusiness(IJoinerQueryService joinerQueryService, ITransactionalService transactionalService)
        {
            this.joinerQueryService = joinerQueryService;
            this.transactionalService = transactionalService;

            this.validation = Validations.ComposeValidations<Task>()
                .Next(Validations.SingleValidation<Task>()
                    .When((task) => task.Id <= 0)
                    .Then(ValidationException.WithMessage<Task>("Task Id is not valid.")))
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

        public Task Update(Task task) 
        {
            this.validation.Evaluate(task);
            transactionalService.Commit();
            
            return task;
        }
    }
}