using AutoMapper;
using CoursesApp.Data.Entities;
using CoursesApp.MapperProfile;
using CoursesApp.Models.Dtos;
using CoursesApp.Repository;
using CoursesApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoursesAppTests.Services
{
    [TestClass()]
    public class CourseServicesTest
    {
        private readonly CourseService _sut;
        private readonly Mock<ICourseRepository> _repositoryMock = new();
        private static MapperConfiguration config = new(x => x.AddProfile(new CourseProfile()));
        private IMapper _mapper = new Mapper(config);

        private readonly Course course1 = new Course()
        {
            CourseId = 1,
            Name = "Bobba",
            Description = "Bet"
        };
        private readonly Course course2 = new Course()
        {
            CourseId = 2,
            Name = "Bibba",
            Description = "Bob"
        };
        private readonly CourseDto courseDto = new CourseDto()
        {
            CourseId = 1,
            Name = "Bobba",
            Description = "Bet"
        };
        public CourseServicesTest()
        {
            _sut = new CourseService(_repositoryMock.Object, _mapper);
        }

        #region Course services tests
        [TestMethod()]
        public void GetCourseListTest()
        {
            IEnumerable<Course> test = new List<Course>() { course1, course2 };

            _repositoryMock.Setup(x => x.GetCoursesList())
                .Returns(test.AsQueryable);

            var result = _sut.GetCoursesList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(courseDto.Name, result[0].Name);
            Assert.AreEqual(courseDto.Description, result[0].Description);
            Assert.AreEqual(courseDto.CourseId, result[0].CourseId);
        }

        [TestMethod()]
        public void CreateCourseTest()
        {
            _repositoryMock.Setup(x => x.CreateCourse("Bobba", "Bob"))
                .Returns(course1);

            var result = _sut.CreateCourse("Bobba", "Bob");

            Assert.AreEqual(courseDto.Name, result.Name);
            Assert.AreEqual(courseDto.Description, result.Description);
            Assert.AreEqual(courseDto.CourseId, result.CourseId);
        }

        [TestMethod()]
        public void UpdateCourseTest()
        {
            _repositoryMock.Setup(x => x.UpdateCourse(2, "Bobba", "Bob"))
                .Returns(course1);

            var result = _sut.UpdateCourse(2, "Bobba", "Bob");


            Assert.AreEqual(courseDto.Name, result.Name);
            Assert.AreEqual(courseDto.Description, result.Description);
            Assert.AreEqual(courseDto.CourseId, result.CourseId);
        }

        [TestMethod()]
        public void DeleteSCourseTest()
        {
            _repositoryMock.Setup(x => x.RemoveCourse(It.IsAny<int>()));
            Assert.AreEqual(0, Environment.ExitCode);
        }
        #endregion

        #region Course services invalid data tests
        [TestMethod()]
        public void CreateCourseInvalidDataTest()
        {
            Assert.IsNull(_sut.CreateCourse(null, null));
        }

        [TestMethod()]
        public void UpdateCourseInvalidDataTest()
        {
            Assert.IsNull(_sut.UpdateCourse(0, null, null));
        }

        [TestMethod()]
        public void DeleteSCourseInvalidDataTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _sut.RemoveCourse(0));
        }
        #endregion
    }
}
