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
    public class StudentControllerTests
    {
        private readonly StudentController _sut;
        private readonly Mock<IStudentService> _servicesMock = new();
        private Mock<ILogger> _logger = new();

        private static readonly StudentDto studentDto1 = new StudentDto()
        {
            StudentId = 1,
            GroupId = 1,
            Name = "Bobba",
            LastName = "Fet"
        };
        private static readonly StudentDto studentDto2 = new StudentDto()
        {
            StudentId = 2,
            GroupId = 1,
            Name = "Bibba",
            LastName = "Bob"
        };
        private readonly IList<StudentDto> studentList = new List<StudentDto>() { studentDto1, studentDto2 };
        public StudentControllerTests()
        {
            _sut = new StudentController(_servicesMock.Object, _logger.Object);
        }

        #region Tests for student controller
        [TestMethod()]
        public void ShowAllStudentsTest()
        {
            _servicesMock.Setup(service => service.GetStudentList()).Returns(studentList);
            var result = _sut.ShowAllStudents() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("StudentView", result.ViewName);
        }

        [TestMethod()]
        public void ShowStudentByGroupIdTest()
        {
            _servicesMock.Setup(service => service.GetStudentByGroupId(1)).Returns(studentList);
            var result = _sut.ShowStudentByGroupId(0) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("StudentView", result.ViewName);
        }

        [TestMethod()]
        public void AddStudentTest()
        {
            _servicesMock.Setup(service => service.CreateStudent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(studentDto2);
            var result = _sut.AddStudent("Bibba", "Bob", 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
        }

        [TestMethod()]
        public void AddStudentViewTest()
        {
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _sut.TempData = tempData;

            _servicesMock.Setup(service => service.GetStudentList()).Returns(studentList);
            var result = _sut.AddStudentView(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("AddStudent", result.ViewName);
        }
        [TestMethod()]
        public void EitStudentTest()
        {
            _servicesMock.Setup(service => service.UpdateStudent(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(studentDto2);
            var result = _sut.EditStudent(studentDto1);

            Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void EditStudentViewTest()
        {
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _sut.TempData = tempData;

            _servicesMock.Setup(service => service.GetStudentList()).Returns(studentList);
            var result = _sut.EditStudentView(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("EditStudent", result.ViewName);
        }
        [TestMethod()]
        public void DeleteStudentTest()
        {
            _servicesMock.Setup(service => service.RemoveStudent(It.IsAny<int>()));
            var result = _sut.DeleteStudent(1);

            Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
        }
        #endregion

        #region Tests for student controller with invalid data
        [TestMethod()]
        public void DeleteStudentInvalidDataTest()
        {
            _servicesMock.Setup(service => service.RemoveStudent(It.IsAny<int>()));
            var result = _sut.DeleteStudent(0);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod()]
        public void EitStudentInvalidDataTest()
        {
            _servicesMock.Setup(service => service.UpdateStudent(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(studentDto1);
            var result = _sut.EditStudent(null);

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }
        [TestMethod()]
        public void AddStudentInvalidDataTest()
        {
            _servicesMock.Setup(service => service.CreateStudent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(new StudentDto());
            var result = _sut.AddStudent("Bibba", "Bob", 0);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }
        [TestMethod()]
        public void ShowStudentByGroupIdInvalidDataTest()
        {
            _servicesMock.Setup(service => service.GetStudentByGroupId(It.IsAny<int>())).Returns(studentList);
            var result = _sut.ShowStudentByGroupId(-1);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }
        #endregion
    }
}
