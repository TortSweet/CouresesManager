using CoursesApp.Data;
using CoursesApp.Data.Entities;
using CoursesApp.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CoursesAppTests.Data
{
    [TestClass()]
    public class CourseRepositoryTest
    {
        private readonly Course courseTest = new Course()
        {
            CourseId = 1,
            Name = "Test course",
            Description = "Test description"
        };
        private readonly ICourseRepository _sut = GetInMemoryCourseRepository();

        private static ICourseRepository GetInMemoryCourseRepository()
        {
            DbContextOptions<AppDBContext> options;
            var builder = new DbContextOptionsBuilder<AppDBContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;

            AppDBContext courseDataContext = new AppDBContext(options);
            courseDataContext.Database.EnsureDeleted();
            courseDataContext.Database.EnsureCreated();
            return new CourseRepository(courseDataContext);
        }

        #region Course repository tests
        [TestMethod()]
        public void GetCourseListTest()
        {
            Course savedCourse1 = _sut.CreateCourse("Test course", "Test description");
            Course savedCourse2 = _sut.CreateCourse("Test course 2", "Test description 2");

            var courseList = _sut.GetCoursesList().ToList();

            Assert.AreEqual(2, courseList.Count);
            Assert.AreEqual(courseTest.CourseId, courseList[0].CourseId);
            Assert.AreEqual(courseTest.Name, courseList[0].Name);
            Assert.AreEqual(courseTest.CourseId, courseList[0].CourseId);
        }

        [TestMethod()]
        public void AddCourseTest()
        {
            Course savedCourse1 = _sut.CreateCourse("Test course", "Test description");
            Course savedCourse2 = _sut.CreateCourse("Test course 2", "Test description 2");

            var courseList = _sut.GetCoursesList().ToList();

            Assert.AreEqual(2, courseList.Count);
            Assert.AreEqual(courseTest.CourseId, courseList[0].CourseId);
            Assert.AreEqual(courseTest.Name, courseList[0].Name);
            Assert.AreEqual(courseTest.CourseId, courseList[0].CourseId);
        }

        [TestMethod()]
        public void UpdateCourseTest()
        {
            Course savedCourse1 = _sut.CreateCourse("Booba", "Test");
            _sut.UpdateCourse(1, "Test course", "Test description");

            Assert.AreEqual(1, _sut.GetCoursesList().Count());
            Assert.AreEqual(courseTest.CourseId, savedCourse1.CourseId);
            Assert.AreEqual(courseTest.Name, savedCourse1.Name);
            Assert.AreEqual(courseTest.Description, savedCourse1.Description);
        }

        [TestMethod()]
        public void DeleteCourseTest()
        {
            Course savedCourse1 = _sut.CreateCourse("Test course", "Test description");
            Course savedCourse2 = _sut.CreateCourse("Test course 2", "Test description 2");

            _sut.RemoveCourse(1);

            Assert.AreEqual(1, _sut.GetCoursesList().Count());
        }

        
        #endregion

        #region Course repository invalid data tests
        [TestMethod()]
        public void UpdateCourseInvalidDataTest()
        {
            Assert.IsNull(_sut.UpdateCourse(0, null, null));
        }

        [TestMethod()]
        public void AddCourseInvalidDataTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _sut.CreateCourse(null, null));
        }

        [TestMethod()]
        public void DeleteCourseInvalidDataTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _sut.RemoveCourse(0));
        }
        #endregion
    }
}
