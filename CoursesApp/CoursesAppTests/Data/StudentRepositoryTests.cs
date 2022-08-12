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
    public class StudentRepositoryTests
    {
        readonly Student studentTest = new Student()
        {
            StudentId = 1,
            GroupId = 1,
            Name = "Bobba",
            LastName = "Fet"
        };
        private static IStudentRepository GetInMemoryStudentRepository()
        {
            DbContextOptions<AppDBContext> options;
            var builder = new DbContextOptionsBuilder<AppDBContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;

            AppDBContext studentDataContext = new AppDBContext(options);
            studentDataContext.Database.EnsureDeleted();
            studentDataContext.Database.EnsureCreated();
            return new StudentRepository(studentDataContext);
        }
        private readonly IStudentRepository _sut = GetInMemoryStudentRepository();

        #region Student repository tests

        [TestMethod()]
        public void GetStudentListTest()
        {
            Student savedStudent1 = _sut.CreateStudent(2, "Bobba", "Fet");
            Student savedStudent2 = _sut.CreateStudent(1, "Tom", "Bebe");
            Student savedStudent3 = _sut.CreateStudent(1, "Harry", "Smit");

            var studentList = _sut.GetStudentList().ToList();

            Assert.AreEqual(3, studentList.Count);
            Assert.AreEqual(studentTest.StudentId, studentList[0].StudentId);
            Assert.AreEqual(studentTest.Name, studentList[0].Name);
            Assert.AreEqual(studentTest.LastName, studentList[0].LastName);
        }

        [TestMethod()]
        public void GetStudentByGroupIdTest()
        {
            Student savedStudent1 = _sut.CreateStudent(2, "Bobba", "Fet");
            Student savedStudent2 = _sut.CreateStudent(1, "Tom", "Bebe");
            Student savedStudent3 = _sut.CreateStudent(1, "Harry", "Smit");

            var studentByGroupId = _sut.GetStudentByGroupId(2).ToList();

            Assert.AreEqual(1, studentByGroupId.Count);
            Assert.AreEqual(studentTest.StudentId, studentByGroupId[0].StudentId);
            Assert.AreEqual(studentTest.Name, studentByGroupId[0].Name);
            Assert.AreEqual(studentTest.LastName, studentByGroupId[0].LastName);
        }

        [TestMethod()]
        public void AddStudentTest()
        {
            Student savedStudent1 = _sut.CreateStudent(1, "Bobba", "Fet");
            Student savedStudent2 = _sut.CreateStudent(1, "Tom", "Bebe");

            Assert.AreEqual(2, _sut.GetStudentList().Count());
            Assert.AreEqual(studentTest.StudentId, savedStudent1.StudentId);
            Assert.AreEqual(studentTest.Name, savedStudent1.Name);
            Assert.AreEqual(studentTest.LastName, savedStudent1.LastName);
        }

        [TestMethod()]
        public void UpdateStudentTest()
        {
            Student savedStudent1 = _sut.CreateStudent(1, "fred", "Blogs");
            _sut.UpdateStudent(1, "Bobba", "Fet");

            Assert.AreEqual(1, _sut.GetStudentList().Count());
            Assert.AreEqual(studentTest.StudentId, savedStudent1.StudentId);
            Assert.AreEqual(studentTest.Name, savedStudent1.Name);
            Assert.AreEqual(studentTest.LastName, savedStudent1.LastName);
        }

        [TestMethod()]
        public void DeleteStudentTest()
        {
            Student savedStudent1 = _sut.CreateStudent(1, "fred", "Blogs");
            Student savedStudent2 = _sut.CreateStudent(1, "Bobba", "Fet");

            _sut.RemoveStudent(1);

            Assert.AreEqual(1, _sut.GetStudentList().Count());
        }
        #endregion

        #region Student repository invalid data tests
        [TestMethod()]
        public void GetStudentByGroupIdInvalidDataTest()
        {
            Assert.IsNull(_sut.GetStudentByGroupId(0));
        }

        [TestMethod()]
        public void AddStudentInvalidData()
        {
            Assert.IsNull(_sut.CreateStudent(0, null, null));
        }

        [TestMethod()]
        public void UpdateStudentInvalidData()
        {
            Assert.IsNull(_sut.UpdateStudent(0, null, null));
        }

        [TestMethod()]
        public void DeleteStudentInvalidData()
        {
            Assert.ThrowsException<ArgumentException>(() => _sut.RemoveStudent(0));
        }
        #endregion
    }
}