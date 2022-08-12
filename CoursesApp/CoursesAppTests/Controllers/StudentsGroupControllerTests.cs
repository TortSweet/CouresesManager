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
    public class StudentsGroupControllerTests
    {
        private readonly GroupController _sut;
        private readonly Mock<IStudGroupService> _servicesMock = new();
        private Mock<ILogger> _logger = new();

        private static readonly StudGroupDto studentGroupDto1 = new StudGroupDto()
        {
            GroupId = 1,
            Name = "Bobba",
            CourseId = 1
        };
        private static readonly StudGroupDto studentGroupDto2 = new StudGroupDto()
        {
            GroupId = 2,
            Name = "Bibba",
            CourseId = 1
        };
        private readonly IList<StudGroupDto> groupList = new List<StudGroupDto>() { studentGroupDto1, studentGroupDto2 };
        public StudentsGroupControllerTests()
        {
            _sut = new GroupController(_servicesMock.Object, _logger.Object);
        }

        #region Tests for student group controller
        [TestMethod()]
        public void ShowAllGroupsTest()
        {
            _servicesMock.Setup(service => service.GetStudGroupList()).Returns(groupList);
            var result = _sut.ShowAllGroup() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("GroupView", result.ViewName);
        }

        [TestMethod()]
        public void ShowGroupsByCourseIdTest()
        {
            _servicesMock.Setup(service => service.GetStudGroupByCourseId(1)).Returns(groupList);
            var result = _sut.ShowGroupById(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("GroupView", result.ViewName);
        }

        [TestMethod()]
        public void AddGroupTest()
        {
            _servicesMock.Setup(service => service.CreateStudGroup(It.IsAny<string>(), It.IsAny<int>())).Returns(studentGroupDto2);
            var result = _sut.AddGroup("Bob", 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
        }

        [TestMethod()]
        public void AddGroupViewTest()
        {
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _sut.TempData = tempData;

            _servicesMock.Setup(service => service.GetStudGroupList()).Returns(groupList);
            var result = _sut.AddGroupView(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("AddGroup", result.ViewName);
        }
        [TestMethod()]
        public void EitGroupTest()
        {
            _servicesMock.Setup(service => service.UpdateStudGroup(It.IsAny<int>(), It.IsAny<string>())).Returns(studentGroupDto2);
            var result = _sut.EditGroup(studentGroupDto1);

            Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void EditGroupViewTest()
        {
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _sut.TempData = tempData;

            _servicesMock.Setup(service => service.GetStudGroupList()).Returns(groupList);
            var result = _sut.EditGroupView(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("EditGroup", result.ViewName);
        }
        [TestMethod()]
        public void DeleteGroupsTest()
        {
            _servicesMock.Setup(service => service.RemoveStudGroup(It.IsAny<int>()));
            var result = _sut.DeleteGroup(1);

            Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
        }
        #endregion

        #region Tests for student group controller with invalid data
        [TestMethod()]
        public void DeleteGroupsInvalidDataTest()
        {
            _servicesMock.Setup(service => service.RemoveStudGroup(It.IsAny<int>()));
            var result = _sut.DeleteGroup(0);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod()]
        public void EitGroupsInvalidDataTest()
        {
            _servicesMock.Setup(service => service.UpdateStudGroup(It.IsAny<int>(), It.IsAny<string>())).Returns(studentGroupDto1);
            var result = _sut.EditGroup(null);

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }
        [TestMethod()]
        public void AddGroupInvalidDataTest()
        {
            _servicesMock.Setup(service => service.CreateStudGroup(It.IsAny<string>(), It.IsAny<int>())).Returns(new StudGroupDto());
            var result = _sut.AddGroup("Bob", 0);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }
        [TestMethod()]
        public void ShowGroupByGroupIdInvalidDataTest()
        {
            _servicesMock.Setup(service => service.GetStudGroupByCourseId(It.IsAny<int>())).Returns(groupList);
            var result = _sut.ShowGroupById(-1);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }
        #endregion
    }
}
