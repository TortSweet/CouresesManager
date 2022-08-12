using CoursesApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CoursesAppTests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private readonly HomeController _sut;
        private Mock<ILogger> _logger = new();

        public HomeControllerTest()
        {
            _sut = new HomeController(_logger.Object);
        }

        [TestMethod()]
        public void IndexTest()
        {
            var result = _sut.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
