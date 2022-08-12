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
    public class StudentServiceTests
    {
        private readonly StudentService _sut;
        private readonly Mock<IStudentRepository> _repositoryMock = new();
        private static MapperConfiguration config = new(x => x.AddProfile(new StudentProfile()));
        private IMapper _mapper = new Mapper(config);

        private readonly Student student1 = new Student()
        {
            StudentId = 1,
            GroupId = 1,
            Name = "Bobba",
            LastName = "Fet"
        };
        Student student2 = new Student()
        {
            StudentId = 2,
            GroupId = 1,
            Name = "Ferrari",
            LastName = "Car"
        };

        private readonly StudentDto studentDto = new StudentDto()
        {
            StudentId = 1,
            GroupId = 1,
            Name = "Bobba",
            LastName = "Fet"
        };

        public StudentServiceTests()
        {
            _sut = new StudentService(_repositoryMock.Object, _mapper);
        }

        #region Student services tests
        [TestMethod()]
        public void DeleteStudentTest()
        {
            _repositoryMock.Setup(x => x.RemoveStudent(It.IsAny<int>()));
            Assert.AreEqual(0, Environment.ExitCode);
        }

        [TestMethod()]
        public void UpdateStudentTest()
        {
            _repositoryMock.Setup(x => x.UpdateStudent(2, "Ferrari", "Car"))
                .Returns(student2);

            var result = _sut.UpdateStudent(2, "Ferrari", "Car");

            
            Assert.AreEqual(student2.Name, result.Name);
            Assert.AreEqual(student2.LastName, result.LastName);
            Assert.AreEqual(student2.StudGroup, result.StudGroup);
            Assert.AreEqual(student2.StudentId, result.StudentId);
        }

        [TestMethod()]
        public void GetStudentListTest()
        {
            IEnumerable<Student> test = new List<Student>() { student1, student2 };

            _repositoryMock.Setup(x => x.GetStudentList())
                .Returns(test.AsQueryable);

            var result = _sut.GetStudentList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(studentDto.Name, result[0].Name);
            Assert.AreEqual(studentDto.LastName, result[0].LastName);
            Assert.AreEqual(studentDto.StudGroup, result[0].StudGroup);
            Assert.AreEqual(studentDto.StudentId, result[0].StudentId);
        }

        [TestMethod()]
        public void GetStudentByGroupIdTest()
        {
            IEnumerable<Student> test = new List<Student>() { student1 };

            _repositoryMock.Setup(x => x.GetStudentByGroupId(1))
                .Returns(test.AsQueryable);

            var result = _sut.GetStudentByGroupId(1);

            Assert.AreEqual(studentDto.Name, result[0].Name);
            Assert.AreEqual(studentDto.LastName, result[0].LastName);
            Assert.AreEqual(studentDto.StudGroup, result[0].StudGroup);
            Assert.AreEqual(studentDto.StudentId, result[0].StudentId);
        }

        [TestMethod()]
        public void CreateStudentTest()
        {
            _repositoryMock.Setup(x => x.CreateStudent(1, "Bobba", "Fet"))
                .Returns(student1);

            var result = _sut.CreateStudent("Bobba", "Fet", 1);

            Assert.AreEqual(studentDto.Name, result.Name);
            Assert.AreEqual(studentDto.LastName, result.LastName);
            Assert.AreEqual(studentDto.StudGroup, result.StudGroup);
            Assert.AreEqual(studentDto.StudentId, result.StudentId);
        }
        #endregion

        #region Student services invalid data tests
        [TestMethod()]
        public void DeleteStudentInvalidDataTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _sut.RemoveStudent(0));
        }

        [TestMethod()]
        public void UpdateStudentInvalidDataTest()
        {
            Assert.IsNull(_sut.UpdateStudent(0, null, null));
        }

        [TestMethod()]
        public void GetStudentByGroupIdInvalidDataTest()
        {
            Assert.IsNull(_sut.GetStudentByGroupId(0));
        }

        [TestMethod()]
        public void CreateStudentInvalidDataTest()
        {
            Assert.IsNull(_sut.CreateStudent(null, null, 0));
        }
        #endregion
    }
}