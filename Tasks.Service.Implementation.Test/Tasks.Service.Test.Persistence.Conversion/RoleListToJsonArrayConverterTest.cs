using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Moq;
using Tasks.Service.Persistence.Conversion;

namespace Tasks.Service.Test.Persistence.Convertion
{
    [TestClass]
    public class RoleListToJsonArrayConverterTest 
    {
        private RoleListToJsonArrayConverter converter;
        private List<string> listToConvert;
        private object result;
        private string valueToConvert;
        private object listObtained;

        [TestInitialize]
        public void SetupTest() {
            this.converter = new RoleListToJsonArrayConverter();
        }

        [TestMethod]
        public void Convert_ListIsNull() {
            GivenANullRoleList();
            WhenListIsConverted();
            ThenReturnANullString();
        }

        [TestMethod]
        public void Convert_ListHasValues() {
            GivenARoleListWithValues();
            WhenListIsConverted();
            ThenReturnAJsonArrayWithExpectedValues();
        }

        [TestMethod]
        public void ConvertBack_ValueIsNull() {
            GivenANullValueFromProvider();
            WhenValueIsConverted();
            ThenReturnANullList();
        }

        [TestMethod]
        public void ConvertBack_ValueHasAJsonArray() {
            GivenAJsonArrayValueFromProvider();
            WhenValueIsConverted();
            ThenReturnAListWithExpectedValues();
        }

        private void GivenAJsonArrayValueFromProvider()
        {
            this.valueToConvert = "[\"role1\",\"role2\",\"role3\"]";
        }

        private void ThenReturnAListWithExpectedValues()
        {
            Assert.IsNotNull(this.listObtained);
            Assert.IsInstanceOfType(this.listObtained, typeof(IList<string>));
            
            var list = this.listObtained as IList<string>;

            Assert.IsTrue(list.All((role) => this.valueToConvert.ToString().Contains(String.Format("\"{0}\"",  role))));
        }

        private void GivenANullValueFromProvider()
        {
            this.valueToConvert = default(string);
        }

        private void WhenValueIsConverted()
        {
            this.listObtained = this.converter.ConvertFromProvider(this.valueToConvert);
        }

        private void ThenReturnANullList()
        {
            Assert.IsNull(this.listObtained);
        }

        private void GivenARoleListWithValues()
        {
            this.listToConvert = new List<string>() { "role1",  "role2" };
        }

        private void ThenReturnAJsonArrayWithExpectedValues()
        {
            Assert.IsInstanceOfType(this.result, typeof(String));

            Assert.IsTrue(this.listToConvert.All((role) => this.result.ToString().Contains(String.Format("\"{0}\"",  role))));
        }

        private void GivenANullRoleList()
        {
            this.listToConvert = default(List<string>);
        }

        private void WhenListIsConverted()
        {
            this.result = converter.ConvertToProvider(this.listToConvert);
        }

        private void ThenReturnANullString()
        {
            Assert.IsNull(this.result);
        }
    }
}