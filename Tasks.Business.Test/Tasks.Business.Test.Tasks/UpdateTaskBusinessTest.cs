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
    public class UpdateTaskBusinessTest
    {
        private Mock<IJoinerQueryService> joinerQueryServiceMock;
        private Mock<ITransactionalService> transactionalServiceMock;
        private UpdateTaskBusiness updateTaskBusiness;

        private Task taskToCreate;
        private Task createdTask;
        private JoinerProfile joinerProfile;
        private bool changesCommited;

        [TestInitialize]
        public void setupTest() {
            joinerQueryServiceMock = new Mock<IJoinerQueryService>();
            transactionalServiceMock = new Mock<ITransactionalService>();

            transactionalServiceMock.Setup((mock) => mock.Commit()).Callback(() => { this.changesCommited = true; });
            joinerQueryServiceMock.Setup((mock) => mock.GetProfile(It.IsAny<long>())).Returns((long value) => this.GetJoinerProfile(value));

            updateTaskBusiness = new UpdateTaskBusiness(
                joinerQueryServiceMock.Object,
                transactionalServiceMock.Object);
        }

        private JoinerProfile GetJoinerProfile(long value)
        {
            return this.joinerProfile;
        }

        [TestMethod]
        public void UpdateTaskFail_TaskIdValidation() {
            GivenATaskWithInvalidId();
            Assert.ThrowsException<ValidationException>(() => WhenTaskIsUpdated());
        }

        [TestMethod]
        public void UpdateTaskFail_NameValidation() {
            GivenATaskWithNullName();
            Assert.ThrowsException<ValidationException>(() => WhenTaskIsUpdated());
        }

        [TestMethod]
        public void UpdateTaskFail_EstimatedTimeValidation() {
            GivenATaskWithEstimatedRequiredHoursLessThanZero();
            Assert.ThrowsException<ValidationException>(() => WhenTaskIsUpdated());
        }

        [TestMethod]
        public void UpdateTaskFail_StackValidation() {
            GivenATaskWithNullStack();
            Assert.ThrowsException<ValidationException>(() => WhenTaskIsUpdated());
        }

        [TestMethod]
        public void UpdateTaskFail_AsigneeIdNumberValueValidation() {
            GivenATaskWithAsigneeIdNumberLessThanZero();
            Assert.ThrowsException<ValidationException>(() => WhenTaskIsUpdated());
        }

        [TestMethod]
        public void UpdateTaskFail_AsigneeValidation() {
            GivenATaskAsignedToANonExistingJoiner();
            Assert.ThrowsException<ValidationException>(() => WhenTaskIsUpdated());
        }

        [TestMethod]
        public void UpdateTaskSuccess() {
            GivenAFullyDefinedTask();
            GivenAnExistingJoiner();
            WhenTaskIsUpdated();
            ThenATaskGetsPersisted();
        }

        private void GivenATaskWithInvalidId()
        {
            this.taskToCreate = new Task() {
                Name = "Task",
                EstimatedRequiredHours = 5,
                Stack = "Stack",
                AssignedIdNumber = 14
            };
        }

        private void ThenATaskGetsPersisted()
        {
            Assert.IsTrue(this.changesCommited);
        }

        private void GivenAnExistingJoiner()
        {
            this.joinerProfile = new JoinerProfile();
        }

        private void GivenAFullyDefinedTask()
        {
            this.taskToCreate = new Task() {
                Id = 1,
                Name = "Task",
                EstimatedRequiredHours = 5,
                Stack = "Stack",
                AssignedIdNumber = 14
            };
        }

        private void GivenATaskAsignedToANonExistingJoiner()
        {
            this.taskToCreate = new Task() {
                Id = 1,
                Name = "Task",
                EstimatedRequiredHours = 5,
                Stack = "Stack",
                AssignedIdNumber = 14
            };
        }

        private void GivenATaskWithAsigneeIdNumberLessThanZero()
        {
            this.taskToCreate = new Task() {
                Id = 1,
                Name = "Task",
                EstimatedRequiredHours = 5,
                Stack = "Stack",
                AssignedIdNumber = -1
            };
        }

        private void GivenATaskWithNullStack()
        {
            this.taskToCreate = new Task() {
                Id = 1,
                Name = "Task",
                EstimatedRequiredHours = 5
            };
        }

        private void GivenATaskWithEstimatedRequiredHoursLessThanZero()
        {
            this.taskToCreate = new Task() {
                Id = 1,
                Name = "Task",
                EstimatedRequiredHours = -1
            };
        }

        private void GivenATaskWithNullName()
        {
            this.taskToCreate = new Task() {
                Id = 1
            };
        }

        private void WhenTaskIsUpdated()
        {
            this.createdTask = this.updateTaskBusiness.Update(this.taskToCreate);
        }
    }
}