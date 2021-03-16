using Tasks.Business.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasks.Service.Tasks;
using Tasks.Service.Joiner;
using Moq;
using Tasks.Service.Persistence;
using Tasks.Model.Domain;
using System;

namespace Tasks.Business.Test.Tasks
{
    [TestClass]
    public class CreateTaskBusinessTest
    {
        private Mock<ITaskRepositoryService> repositoryServiceMock;
        private Mock<IJoinerQueryService> joinerQueryServiceMock;
        private Mock<ITransactionalService> transactionalServiceMock;
        private CreateTaskBusiness createTaskBusiness;

        private Task taskToCreate;
        private Task createdTask;
        private JoinerProfile joinerProfile;

        [TestInitialize]
        public void setupTest() {
            repositoryServiceMock = new Mock<ITaskRepositoryService>();
            joinerQueryServiceMock = new Mock<IJoinerQueryService>();
            transactionalServiceMock = new Mock<ITransactionalService>();

            transactionalServiceMock.Setup((mock) => mock.Commit()).Callback(() => {  });
            repositoryServiceMock.Setup((mock) => mock.Persist(It.IsAny<Task>())).Returns((Task value) => value);
            joinerQueryServiceMock.Setup((mock) => mock.GetProfile(It.IsAny<long>())).Returns((long value) => this.GetJoinerProfile(value));

            createTaskBusiness = new CreateTaskBusiness(
                repositoryServiceMock.Object,
                joinerQueryServiceMock.Object,
                transactionalServiceMock.Object);
        }

        private JoinerProfile GetJoinerProfile(long value)
        {
            return this.joinerProfile;
        }

        [TestMethod]
        public void CreateTaskFail_NameValidation() {
            GivenATaskWithNullName();
            Assert.ThrowsException<ValidationException>(() => WhenTaskGetsCreated());
        }

        [TestMethod]
        public void CreateTaskFail_EstimatedTimeValidation() {
            GivenATaskWithEstimatedRequiredHoursLessThanZero();
            Assert.ThrowsException<ValidationException>(() => WhenTaskGetsCreated());
        }

        [TestMethod]
        public void CreateTaskFail_StackValidation() {
            GivenATaskWithNullStack();
            Assert.ThrowsException<ValidationException>(() => WhenTaskGetsCreated());
        }

        [TestMethod]
        public void CreateTaskFail_AsigneeIdNumberValueValidation() {
            GivenATaskWithAsigneeIdNumberLessThanZero();
            Assert.ThrowsException<ValidationException>(() => WhenTaskGetsCreated());
        }

        [TestMethod]
        public void CreateTaskFail_AsigneeValidation() {
            GivenATaskAsignedToANonExistingJoiner();
            Assert.ThrowsException<ValidationException>(() => WhenTaskGetsCreated());
        }

        [TestMethod]
        public void CreateTaskSuccess() {
            GivenAFullyDefinedTask();
            GivenAnExistingJoiner();
            WhenTaskGetsCreated();
            ThenATaskGetsPersisted();
        }

        private void ThenATaskGetsPersisted()
        {
            Assert.IsNotNull(this.createdTask);
            Assert.AreEqual(this.taskToCreate, this.createdTask);
        }

        private void GivenAnExistingJoiner()
        {
            this.joinerProfile = new JoinerProfile();
        }

        private void GivenAFullyDefinedTask()
        {
            this.taskToCreate = new Task() {
                Name = "Task",
                EstimatedRequiredHours = 5,
                Stack = "Stack",
                AssignedIdNumber = 14
            };
        }

        private void GivenATaskAsignedToANonExistingJoiner()
        {
            this.taskToCreate = new Task() {
                Name = "Task",
                EstimatedRequiredHours = 5,
                Stack = "Stack",
                AssignedIdNumber = 14
            };
        }

        private void GivenATaskWithAsigneeIdNumberLessThanZero()
        {
            this.taskToCreate = new Task() {
                Name = "Task",
                EstimatedRequiredHours = 5,
                Stack = "Stack",
                AssignedIdNumber = -1
            };
        }

        private void GivenATaskWithNullStack()
        {
            this.taskToCreate = new Task() {
                Name = "Task",
                EstimatedRequiredHours = 5
            };
        }

        private void GivenATaskWithEstimatedRequiredHoursLessThanZero()
        {
            this.taskToCreate = new Task() {
                Name = "Task",
                EstimatedRequiredHours = -1
            };
        }

        private void GivenATaskWithNullName()
        {
            this.taskToCreate = new Task() {
            };
        }

        private void WhenTaskGetsCreated()
        {
            this.createdTask = this.createTaskBusiness.Create(this.taskToCreate);
        }
    }
}