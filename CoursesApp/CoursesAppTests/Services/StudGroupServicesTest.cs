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
    public class StudGroupServicesTest
    {
        private readonly StudGroupService _sut;
        private readonly Mock<IStudGroupRepository> _repositoryMock = new();
        private static MapperConfiguration config = new(x => x.AddProfile(new StudentGroupProfile()));
        private IMapper _mapper = new Mapper(config);

        private readonly StudGroup studGroup1 = new StudGroup()
        {
            GroupId = 1,
            Name = "Bobba",
            CourseId = 1
        };
        private readonly StudGroup studGroup2 = new StudGroup()
        {
            GroupId = 2,
            Name = "Bibba",
            CourseId = 1
        };

        private readonly StudGroupDto studentGroupDto = new StudGroupDto()
        {
            GroupId = 1,
            Name = "Bobba",
            CourseId = 1
        };

        public StudGroupServicesTest()
        {
            _sut = new StudGroupService(_repositoryMock.Object, _mapper);
        }
        
        #region Student group services tests
        [TestMethod()]
        public void GetStudentGroupListTest()
        {
            IEnumerable<StudGroup> test = new List<StudGroup>() { studGroup1, studGroup2 };

            _repositoryMock.Setup(x => x.GetStudGroupsList())
                .Returns(test.AsQueryable);

            var result = _sut.GetStudGroupList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(studentGroupDto.Name, result[0].Name);
            Assert.AreEqual(studentGroupDto.GroupId, result[0].GroupId);
            Assert.AreEqual(studentGroupDto.CourseId, result[0].CourseId);
        }

        [TestMethod()]
        public void GetStudentByGroupIdTest()
        {
            IEnumerable<StudGroup> test = new List<StudGroup>() { studGroup1 };

            _repositoryMock.Setup(x => x.GetStudGroupByCourseId(1))
                .Returns(test.AsQueryable);

            var result = _sut.GetStudGroupByCourseId(1);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(studentGroupDto.Name, result[0].Name);
            Assert.AreEqual(studentGroupDto.GroupId, result[0].GroupId);
            Assert.AreEqual(studentGroupDto.CourseId, result[0].CourseId);
        }

        [TestMethod()]
        public void CreateStudentGroupTest()
        {
            _repositoryMock.Setup(x => x.CreateStudGroup(1, "Bobba"))
                .Returns(studGroup1);

            var result = _sut.CreateStudGroup("Bobba", 1);

            Assert.AreEqual(studentGroupDto.Name, result.Name);
            Assert.AreEqual(studentGroupDto.GroupId, result.GroupId);
            Assert.AreEqual(studentGroupDto.CourseId, result.CourseId);
        }

        [TestMethod()]
        public void UpdateStudentGroupTest()
        {
            _repositoryMock.Setup(x => x.UpdateStudGroup(2, "Ferrari"))
                .Returns(studGroup2);

            var result = _sut.UpdateStudGroup(2, "Ferrari");


            Assert.AreEqual(studGroup2.Name, result.Name);
            Assert.AreEqual(studGroup2.GroupId, result.GroupId);
        }

        [TestMethod()]
        public void DeleteStudentGroupTest()
        {
            _repositoryMock.Setup(x => x.RemoveStudGroup(It.IsAny<int>()));
            Assert.AreEqual(0, Environment.ExitCode);
        }
        #endregion

        #region Student group services invalid data tests
        
        [TestMethod()]
        public void GetStudentByGroupIdInvalidDataTest()
        {
            Assert.IsNull(_sut.GetStudGroupByCourseId(0));
        }

        [TestMethod()]
        public void CreateStudentGroupInvalidDataTest()
        {
            Assert.IsNull(_sut.CreateStudGroup(null, 0));
        }

        [TestMethod()]
        public void UpdateStudentGroupInvalidDataTest()
        {
            Assert.IsNull(_sut.UpdateStudGroup(0, null));
        }

        [TestMethod()]
        public void DeleteStudentGroupInvalidDataTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _sut.RemoveStudGroup(0));
        }
        #endregion

    }
}
