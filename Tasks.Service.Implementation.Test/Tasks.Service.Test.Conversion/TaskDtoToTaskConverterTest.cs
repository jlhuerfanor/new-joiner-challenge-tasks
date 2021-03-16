using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tasks.Model.Client;
using Tasks.Model.Domain;
using Tasks.Service.Conversion;
using Tasks.Service.Tasks;

namespace Tasks.Service.Test.Conversion
{
    [TestClass]
    public class TaskDtoToTaskConverterTest
    {
        private Mock<ITaskQueryService> serviceMock;
        private TaskDtoToTaskConverter converter;
        private Dictionary<int, Task> taskById;
        private TaskDto dtoToConvert;
        private Task entityConverted;
        private Task parentTask;
        private Task task;

        [TestInitialize]
        public void SetupTest() {
            serviceMock = new Mock<ITaskQueryService>();
            serviceMock.Setup((mock) => mock.GetById(It.IsAny<int>())).Returns((int id) => this.GetTaskById(id));

            converter = new TaskDtoToTaskConverter(serviceMock.Object);
            taskById = new Dictionary<int, Task>();
        }

        [TestMethod]
        public void ConvertTaskDto_NullId_NullParentId() {
            GivenADtoWithoutIdAndWithoutParent();
            WhenDtoGetsConvertedToEntity();
            ThenAllDtoFieldsMatch();
        }

        [TestMethod]
        public void ConvertTaskDto_NullId_DefinedParentId() {
            GivenADtoWithoutIdAndWithParentId();
            GivenACorrespondingParentTask();
            WhenDtoGetsConvertedToEntity();
            ThenAllDtoFieldsMatch();
            ThenDtoParentMatch();
        }

        [TestMethod]
        public void ConvertTaskDto_DefinedId_NullParentId() {
            GivenADtoWithIdAndWithoutParent();
            GivenACorrespondingTaskInRepository();
            WhenDtoGetsConvertedToEntity();
            ThenAllDtoFieldsMatch();
            ThenItHasTheSameId();
        }

        [TestMethod]
        public void ConvertTaskDto_DefinedId_DefinedParentId() {
            GivenADtoWithIdAndWithParentId();
            GivenACorrespondingParentTask();
            GivenACorrespondingTaskInRepository();
            WhenDtoGetsConvertedToEntity();
            ThenAllDtoFieldsMatch();
            ThenDtoParentMatch();
            ThenItHasTheSameId();
        }
        
        private void GivenACorrespondingTaskInRepository()
        {
            this.task = new Task() { Id = 18, Name = "TheTask", EstimatedRequiredHours = 12, Stack = "2", AssignedIdNumber = 34 };
            this.taskById.Add(18, this.task);
        }

        private void GivenADtoWithIdAndWithParentId()
        {
            this.dtoToConvert = new TaskDto() {
                Id = 18,
                Name = "Task",
                Description = "Description",
                Completed = false,
                EstimatedRequiredHours = 14,
                Stack = "Stack",
                MinimumRoles = new List<string>() { "role1", "role2" },
                AssignedIdNumber = 14,
                ParentTaskId = 14
            };
        }

        private void GivenADtoWithIdAndWithoutParent()
        {
            this.dtoToConvert = new TaskDto() {
                Id = 18,
                Name = "Task",
                Description = "Description",
                Completed = false,
                EstimatedRequiredHours = 14,
                Stack = "Stack",
                MinimumRoles = new List<string>() { "role1", "role2" },
                AssignedIdNumber = 14
            };
        }

        private void ThenItHasTheSameId()
        {
            Assert.AreEqual(this.dtoToConvert.Id.Value, this.entityConverted.Id);
        }

        private void ThenDtoParentMatch()
        {
            Assert.AreEqual(this.parentTask, this.entityConverted.ParentTask);
        }

        private void GivenADtoWithoutIdAndWithParentId()
        {
            this.dtoToConvert = new TaskDto() {
                Name = "Task",
                Description = "Description",
                Completed = false,
                EstimatedRequiredHours = 14,
                Stack = "Stack",
                MinimumRoles = new List<string>() { "role1", "role2" },
                AssignedIdNumber = 14,
                ParentTaskId = 14
            };
        }

        private void GivenACorrespondingParentTask()
        {
            this.parentTask = new Task() { Id = 14 };
            this.taskById.Add(14, this.parentTask);
        }

        private void GivenADtoWithoutIdAndWithoutParent()
        {
            this.dtoToConvert = new TaskDto() {
                Name = "Task",
                Description = "Description",
                Completed = false,
                EstimatedRequiredHours = 14,
                Stack = "Stack",
                MinimumRoles = new List<string>() { "role1", "role2" },
                AssignedIdNumber = 14,
                ParentTaskId = null
            };
        }

        private void WhenDtoGetsConvertedToEntity()
        {
            this.entityConverted = this.converter.Convert(this.dtoToConvert, null, null);
        }

        private void ThenAllDtoFieldsMatch()
        {
            Assert.AreEqual(this.dtoToConvert.Id.HasValue ? this.dtoToConvert.Id.Value : 0, this.entityConverted.Id);
            Assert.AreEqual(this.dtoToConvert.Name, this.entityConverted.Name);
            Assert.AreEqual(this.dtoToConvert.Description, this.entityConverted.Description);
            Assert.AreEqual(this.dtoToConvert.Completed, this.entityConverted.Completed);
            Assert.AreEqual(this.dtoToConvert.EstimatedRequiredHours, this.entityConverted.EstimatedRequiredHours);
            Assert.AreEqual(this.dtoToConvert.Stack, this.entityConverted.Stack);
            Assert.AreEqual(this.dtoToConvert.AssignedIdNumber, this.entityConverted.AssignedIdNumber);

            Assert.IsNotNull(this.entityConverted.MinimumRoles);
            Assert.AreEqual(2, this.entityConverted.MinimumRoles.Count);
            Assert.IsTrue(this.entityConverted.MinimumRoles.All((s) => this.dtoToConvert.MinimumRoles.Contains(s)));
        }

        private Task GetTaskById(int id)
        {
            return this.taskById.ContainsKey(id) ? this.taskById[id] : null;
        }
    }
    
}