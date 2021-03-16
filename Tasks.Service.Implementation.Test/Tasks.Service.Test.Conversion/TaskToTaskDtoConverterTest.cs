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
    public class TaskToTaskDtoConverterTest
    {
        private Mock<ITaskQueryService> serviceMock;
        private TaskDtoToTaskConverter converter;
        private TaskDto dtoConverted;
        private Task entityToConvert;

        [TestInitialize]
        public void SetupTest() {
            serviceMock = new Mock<ITaskQueryService>();
            converter = new TaskDtoToTaskConverter(serviceMock.Object);
        }
    
        [TestMethod]
        public void ConvertEntity_NoParentTask() {
            GivenATaskWithoutParent();
            WhenTaskGetsConverted();
            ThenAllFieldsMatch();
        }

        [TestMethod]
        public void ConvertEntity_WithParentTask() {
            GivenATaskWithAParentTask();
            WhenTaskGetsConverted();
            ThenAllFieldsMatch();
            ThenParentTaskIdMatch();
        }

        private void GivenATaskWithAParentTask()
        {
            this.entityToConvert = new Task() {
                Id = 18,
                Name = "Task",
                Description = "Description",
                Completed = false,
                EstimatedRequiredHours = 14,
                Stack = "Stack",
                MinimumRoles = new List<string>() { "role1", "role2" },
                AssignedIdNumber = 14,
                ParentTask = new Task() {
                    Id = 14
                }
            };
        }

        private void ThenParentTaskIdMatch()
        {
            Assert.AreEqual(this.entityToConvert.ParentTask.Id, this.dtoConverted.ParentTaskId);
        }

        private void GivenATaskWithoutParent()
        {
            this.entityToConvert = new Task() {
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

        private void WhenTaskGetsConverted()
        {
            this.dtoConverted = this.converter.Convert(this.entityToConvert, null, null);
        }

        private void ThenAllFieldsMatch()
        {
            Assert.AreEqual(this.entityToConvert.Id, this.dtoConverted.Id);
            Assert.AreEqual(this.entityToConvert.Name, this.dtoConverted.Name);
            Assert.AreEqual(this.entityToConvert.Description, this.dtoConverted.Description);
            Assert.AreEqual(this.entityToConvert.Completed, this.dtoConverted.Completed);
            Assert.AreEqual(this.entityToConvert.EstimatedRequiredHours, this.dtoConverted.EstimatedRequiredHours);
            Assert.AreEqual(this.entityToConvert.Stack, this.dtoConverted.Stack);
            Assert.AreEqual(this.entityToConvert.AssignedIdNumber, this.dtoConverted.AssignedIdNumber);

            Assert.IsNotNull(this.dtoConverted.MinimumRoles);
            Assert.AreEqual(2, this.dtoConverted.MinimumRoles.Count);
            Assert.IsTrue(this.dtoConverted.MinimumRoles.All((s) => this.entityToConvert.MinimumRoles.Contains(s)));
        }
    }
    
}