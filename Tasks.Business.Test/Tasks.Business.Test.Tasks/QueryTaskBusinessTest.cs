using System;
using System.Linq;
using Tasks.Business.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tasks.Service.Tasks;
using System.Collections.Generic;
using Tasks.Model.Domain;

namespace Tasks.Business.Test.Tasks
{
    [TestClass]
    public class QueryTaskBusinessTest
    {
        private Mock<ITaskQueryService> taskQueryServiceMock;
        private QueryTaskBusiness queryTaskBusiness;

        private List<Task> tasksInRepository;
        private IList<int> result;

        [TestInitialize]
        public void init() {
            tasksInRepository = new List<Task>();
            taskQueryServiceMock = new Mock<ITaskQueryService>();

            taskQueryServiceMock.Setup(service => service.GetAllTasks()).Returns(() => this.tasksInRepository);
            queryTaskBusiness = new QueryTaskBusiness(taskQueryServiceMock.Object);
        }

        [TestMethod]
        public void GetTaskIds_EmptyList() {
            GivenAnEmptyRepository();
            WhenGetTaskIds();
            ThenResultIsEmpty();
        }

        [TestMethod]
        public void GetTaskIds_ListOfIds() {
            GivenARepositoryWithTasks();
            WhenGetTaskIds();
            ThenResultIsExpected();
        }

        private void GivenARepositoryWithTasks()
        {
            this.tasksInRepository.Add(new Task() { Id = 1 });
            this.tasksInRepository.Add(new Task() { Id = 2 });
            this.tasksInRepository.Add(new Task() { Id = 3 });
            this.tasksInRepository.Add(new Task() { Id = 4 });
            this.tasksInRepository.Add(new Task() { Id = 5 });
        }

        private void ThenResultIsExpected()
        {
            Assert.IsNotNull(this.result);
            Assert.AreEqual(this.tasksInRepository.Count, this.result.Count);
            Assert.IsTrue(this.tasksInRepository.All(value => this.result.Contains(value.Id)));
        }

        private void GivenAnEmptyRepository()
        {
            this.tasksInRepository.Clear();
        }

        private void WhenGetTaskIds()
        {
            this.result = this.queryTaskBusiness.GetTaskIds();
        }

        private void ThenResultIsEmpty()
        {
            Assert.IsNotNull(this.result);
            Assert.IsTrue(this.result.Count == 0);
        }
    }
}