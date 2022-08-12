using CoursesApp.Controllers;
using CoursesApp.Models.Dtos;
using CoursesApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CoursesAppTests.Controllers
{
    [TestClass()]
    public class CourseControllerTests
    {
        private readonly CourseController _sut;
        private readonly Mock<ICourseService> _servicesMock = new();
        private Mock<ILogger> _logger = new();

        private static readonly CourseDto courseDto1 = new CourseDto()
        {
            CourseId = 1,
            Name = "Bobba",
            Description = "Test"
        };
        private static readonly CourseDto courseDto2 = new CourseDto()
        {
            CourseId = 2,
            Name = "Bibba",
            Description = "Test2"
        };
        private readonly List<CourseDto> courseList = new List<CourseDto>() { courseDto1, courseDto2 };
        public CourseControllerTests()
        {
            _sut = new CourseController(_servicesMock.Object, _logger.Object);
        }

        #region Tests for course controller
        [TestMethod()]
        public void ShowAllCourseTest()
        {
            _servicesMock.Setup(service => service.GetCoursesList()).Returns(courseList);
            var result = _sut.ShowCourses() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("CourseView", result.ViewName);
        }

        [TestMethod()]
        public void AddCourseTest()
        {
            _servicesMock.Setup(service => service.CreateCourse(It.IsAny<string>(), It.IsAny<string>())).Returns(courseDto2);
            var result = _sut.AddCourse("Bob", "Test2");

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
        }

        [TestMethod()]
        public void AddCourseViewTest()
        {
            _servicesMock.Setup(service => service.GetCoursesList()).Returns(courseList);
            var result = _sut.AddCourseView() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("AddCourse", result.ViewName);
        }
        [TestMethod()]
        public void EitCourseTest()
        {
            _servicesMock.Setup(service => service.UpdateCourse(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(courseDto2);
            var result = _sut.EditCourse(courseDto1);

            Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void EditCourseViewTest()
        {
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _sut.TempData = tempData;

            _servicesMock.Setup(service => service.GetCoursesList()).Returns(courseList);
            var result = _sut.EditCourseView(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("EditCourse", result.ViewName);
        }
        [TestMethod()]
        public void DeleteCourseTest()
        {
            _servicesMock.Setup(service => service.RemoveCourse(It.IsAny<int>()));
            var result = _sut.DeleteCourse(1) as ActionResult;

            Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
        }
        #endregion

        #region Tests for student controller with invalid data
        [TestMethod()]
        public void DeleteCourseInvalidDataTest()
        {
            _servicesMock.Setup(service => service.RemoveCourse(It.IsAny<int>()));
            var result = _sut.DeleteCourse(0);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod()]
        public void EitCourseInvalidDataTest()
        {
            _servicesMock.Setup(service => service.UpdateCourse(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(courseDto1);
            var result = _sut.EditCourse(null);

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }
        [TestMethod()]
        public void AddCourseInvalidDataTest()
        {
            _servicesMock.Setup(service => service.CreateCourse(It.IsAny<string>(), It.IsAny<string>())).Returns(new CourseDto());
            var result = _sut.AddCourse(null, null);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }
        #endregion
    }
}