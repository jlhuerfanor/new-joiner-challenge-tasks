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
    public class DeleteTaskBusinessTest
    {
        private Mock<ITaskRepositoryService> repositoryServiceMock;
        private Mock<ITaskQueryService> taskQueryServiceMock;
        private Mock<ITransactionalService> transactionalServiceMock;
        private DeleteTaskBusiness deleteTaskBusiness;

        private Task deletedTask;
        private Task taskToDelete;
        private int taskIdToDelete;

        [TestInitialize]
        public void setupTest() {
            repositoryServiceMock = new Mock<ITaskRepositoryService>();
            taskQueryServiceMock = new Mock<ITaskQueryService>();
            transactionalServiceMock = new Mock<ITransactionalService>();

            transactionalServiceMock.Setup((mock) => mock.Commit()).Callback(() => {  });
            repositoryServiceMock.Setup((mock) => mock.Delete(It.IsAny<Task>())).Callback((Task value) => this.deletedTask = value);
            taskQueryServiceMock.Setup((mock) => mock.GetById(It.IsAny<int>())).Returns((long value) => this.GetTaskById(value));

            deleteTaskBusiness = new DeleteTaskBusiness(
                repositoryServiceMock.Object,
                taskQueryServiceMock.Object,
                transactionalServiceMock.Object);
        }

        private Task GetTaskById(long value)
        {
            return this.taskToDelete;
        }

        [TestMethod]
        public void DeleteTaskFail_TaskNotFound() {
            GivenANotExistingTaskId();
            Assert.ThrowsException<ValidationException>(() => WhenDeleteTheTaskById());
        }

        [TestMethod]
        public void DeleteTaskSuccess() {
            GivenAnExistingTaskId();
            WhenDeleteTheTaskById();
            ThenTheTaskIsDeleted();
        }

        private void GivenAnExistingTaskId()
        {
            this.taskIdToDelete = 12;
            this.taskToDelete = new Task() { Id = 12 };
        }

        private void ThenTheTaskIsDeleted()
        {
            Assert.IsNotNull(this.deletedTask);
            Assert.AreEqual(this.taskIdToDelete, this.deletedTask.Id);
            Assert.AreEqual(this.taskToDelete, this.deletedTask);
        }

        private void WhenDeleteTheTaskById()
        {
            this.deleteTaskBusiness.Delete(this.taskIdToDelete);
        }

        private void GivenANotExistingTaskId()
        {
            this.taskIdToDelete = 1;
        }
    }
}