using System;
using CoursesApp.Data.Entities;
using CoursesApp.Repository;
using System.Linq;

namespace CoursesApp.Data
{
    public class StudGroupRepository : IStudGroupRepository
    {
        private readonly AppDBContext _context;

        public StudGroupRepository(AppDBContext context)
        {
            _context = context ?? throw new ArgumentNullException("AppDbContext can't be null", nameof(context));
        }
        public StudGroup CreateStudGroup(int courseId, string name)
        {
            if (courseId <= 0 || name == null)
            {
                return null;
            }
            var newStudGroup = new StudGroup(){Name = name, CourseId = courseId};

            _context.StudGroups.Add(newStudGroup);
            _context.SaveChanges();

            return newStudGroup;
        }

        public IQueryable<StudGroup> GetStudGroupByCourseId(int courseId)
        {
            if (courseId <= 0)
            {
                return null;
            }

            var groups = (from b in _context.StudGroups where courseId == b.CourseId select b);
            return groups;
        }

        public IQueryable<StudGroup> GetStudGroupsList()
        {
            return _context.StudGroups;
        }

        public void RemoveStudGroup(int groupId)
        {
            if (groupId <= 0)
            {
                throw new ArgumentException("Id can't be less or equal than 0", nameof(groupId));
            }
            var group = _context.StudGroups.FirstOrDefault(x => x.GroupId == groupId);
            var students = _context.Students.Count(x => x.GroupId == groupId);
            if(students == 0)
            {
                _context.StudGroups.Remove(group);
                _context.SaveChanges();
            }            
        }

        public StudGroup UpdateStudGroup(int groupId, string name)
        {
            if (groupId <= 0 || name == null)
            {
                return null;
            }
            var group = _context.StudGroups.FirstOrDefault(e => e.GroupId == groupId);
            group.Name = name;
            _context.SaveChanges();
            return group;
        }
    }
}
