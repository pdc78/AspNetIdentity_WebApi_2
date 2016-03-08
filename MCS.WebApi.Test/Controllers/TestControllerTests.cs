using System;
using AspNetIdentity_WebApi.Controllers;
using NUnit.Core;
//using NUnit.Core;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace MCS.WebApi.Test.Controllers
{
    [TestFixture]
    public class TestControllerTests
    {
        [Test]
        public void GetTest()
        {
            //Arrange
            Assert.IsTrue(true);
        }

        [Test]
        public void Verify_GetTest()
        {
            //Arrange
            var tController = new TestController();

            var result = tController.Get();

            // Assert
            Assert.IsNotNull(result);

        }
    }
}