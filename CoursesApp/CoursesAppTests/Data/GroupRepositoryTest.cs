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
    public class GroupRepositoryTest
    {
        readonly StudGroup groupTest = new StudGroup()
        {
            CourseId = 1,
            GroupId = 1,
            Name = "Test group",
        };
        private readonly IStudGroupRepository _sut = GetInMemoryStudGroupRepository();

        private static IStudGroupRepository GetInMemoryStudGroupRepository()
        {
            DbContextOptions<AppDBContext> options;
            var builder = new DbContextOptionsBuilder<AppDBContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;

            AppDBContext studGroupDataContext = new AppDBContext(options);
            studGroupDataContext.Database.EnsureDeleted();
            studGroupDataContext.Database.EnsureCreated();
            return new StudGroupRepository(studGroupDataContext);
        }

        #region Group repository tests
        [TestMethod()]
        public void GetGroupListTest()
        {
            StudGroup savedGroup1 = _sut.CreateStudGroup(1, "Test group");
            StudGroup savedGroup2 = _sut.CreateStudGroup(2, "Test group 2");

            var studentByGroupId = _sut.GetStudGroupsList().ToList();

            Assert.AreEqual(2, studentByGroupId.Count);
            Assert.AreEqual(groupTest.CourseId, studentByGroupId[0].CourseId);
            Assert.AreEqual(groupTest.Name, studentByGroupId[0].Name);
            Assert.AreEqual(groupTest.GroupId, studentByGroupId[0].GroupId);
        }

        [TestMethod()]
        public void GetGroupByCourseIdTest()
        {
            StudGroup savedGroup1 = _sut.CreateStudGroup(1, "Test group");
            StudGroup savedGroup2 = _sut.CreateStudGroup(2, "Test group 2");

            var studentByGroupId = _sut.GetStudGroupByCourseId(1).ToList();

            Assert.AreEqual(1, studentByGroupId.Count);
            Assert.AreEqual(groupTest.CourseId, studentByGroupId[0].CourseId);
            Assert.AreEqual(groupTest.Name, studentByGroupId[0].Name);
            Assert.AreEqual(groupTest.GroupId, studentByGroupId[0].GroupId);
        }

        [TestMethod()]
        public void AddGroupTest()
        {
            StudGroup savedGroup1 = _sut.CreateStudGroup(1, "Test group");
            StudGroup savedGroup2 = _sut.CreateStudGroup(2, "Test group 2");

            Assert.AreEqual(2, _sut.GetStudGroupsList().Count());
            Assert.AreEqual(groupTest.CourseId, savedGroup1.CourseId);
            Assert.AreEqual(groupTest.Name, savedGroup1.Name);
            Assert.AreEqual(groupTest.GroupId, savedGroup1.GroupId);
        }

        [TestMethod()]
        public void UpdateGroupTest()
        {
            StudGroup savedGroup1 = _sut.CreateStudGroup(1, "Booba");
            _sut.UpdateStudGroup(1, "Test group");

            Assert.AreEqual(1, _sut.GetStudGroupsList().Count());
            Assert.AreEqual(groupTest.CourseId, savedGroup1.CourseId);
            Assert.AreEqual(groupTest.Name, savedGroup1.Name);
            Assert.AreEqual(groupTest.GroupId, savedGroup1.GroupId);
        }

        [TestMethod()]
        public void DeleteGroupTest()
        {
            StudGroup savedGroup1 = _sut.CreateStudGroup(1, "Test group");
            StudGroup savedGroup2 = _sut.CreateStudGroup(2, "Test group 2");

            _sut.RemoveStudGroup(1);

            Assert.AreEqual(1, _sut.GetStudGroupsList().Count());
        }
        #endregion

        #region Group repository tests
        [TestMethod()]
        public void GetGroupByCourseIdInvalidDataTest()
        {
            Assert.IsNull(_sut.GetStudGroupByCourseId(0));
        }

        [TestMethod()]
        public void AddGroupInvalidDataTest()
        {
            Assert.IsNull(_sut.CreateStudGroup(0, null));
        }

        [TestMethod()]
        public void UpdateGroupInvalidDataTest()
        {
            Assert.IsNull(_sut.UpdateStudGroup(0, null));
        }

        [TestMethod()]
        public void DeleteGroupInvalidDataTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _sut.RemoveStudGroup(0));
        }
        #endregion
    }
}
