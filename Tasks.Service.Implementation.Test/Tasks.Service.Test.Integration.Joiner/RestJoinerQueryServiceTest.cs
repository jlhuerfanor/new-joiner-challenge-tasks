using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Moq;
using Tasks.Service.Joiner;
using Tasks.Model.Domain;

namespace Tasks.Service.Test.Integration.Joiner
{
    [TestClass]
    public class RestJoinerQueryServiceTest
    {
        private const string TestServiceUrl = "https://new-joiners.herokuapp.com/wap/new-joiners/joiner";
        private RestJoinerQueryService service;
        private long idNumber;
        private JoinerProfile profileReceived;

        [TestInitialize]
        public void SetupTest() {
            var loggerMock  = new Mock<ILogger<RestJoinerQueryService>>();

            loggerMock.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            service = new RestJoinerQueryService(loggerMock.Object, new JsonSerializerOptions(JsonSerializerDefaults.Web) { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }, TestServiceUrl,  5000);
        }

        [TestMethod]
        public void GetById_Success() {
            GivenAValidIdNumber();
            WhenJoinerProfileIsRequestedByIdNumber();
            ThenACorrespondingProfileIsReceived();
        }

        [TestMethod]
        public void GetById_IdNumberNotFound_ReturnsNull() {
            GivenAnInvalidIdNumber();
            WhenJoinerProfileIsRequestedByIdNumber();
            ThenANullProfileIsReceived();
        }

        private void GivenAnInvalidIdNumber()
        {
            this.idNumber = 100000;
        }

        private void ThenANullProfileIsReceived()
        {
            Assert.IsNull(this.profileReceived);
        }

        private void GivenAValidIdNumber()
        {
            this.idNumber = 1023456789L;
        }

        private void WhenJoinerProfileIsRequestedByIdNumber()
        {
            this.profileReceived = this.service.GetProfile(this.idNumber);
        }

        private void ThenACorrespondingProfileIsReceived()
        {
            Assert.IsNotNull(this.profileReceived);
            Console.WriteLine(this.profileReceived.ToString());
            Assert.AreEqual(this.idNumber.ToString(), this.profileReceived.IdNumber);
        }

        private void LogErrors(Exception ex, string message, object[] args)
        {
            Console.WriteLine(message, args);
            Console.WriteLine(ex);
        }
    }
    
}