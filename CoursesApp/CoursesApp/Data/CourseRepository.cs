using System;
using CoursesApp.Data.Entities;
using CoursesApp.Repository;
using System.Linq;

namespace CoursesApp.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDBContext _context;

        public CourseRepository(AppDBContext context)
        {
            _context = context ?? throw new ArgumentNullException("AppDbContext can't be null", nameof(context));
        }

        public Course CreateCourse(string name, string description)
        {
            if (name == null || description == null)
            {
                throw new ArgumentNullException("Input params can't be null");
            }

            var newCourse = new Course(){Name = name, Description = description};

            _context.Courses.Add(newCourse);
            _context.SaveChanges();

            return newCourse;
        }

        public IQueryable<Course> GetCoursesList()
        {
            return _context.Courses.AsQueryable();
        }

        public void RemoveCourse(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id can't be less or equal than 0", nameof(id));
            }
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == id);
            var groups = _context.StudGroups.Count(x => x.CourseId == id);
            if (groups == 0)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }

        public Course UpdateCourse(int id, string name, string description)
        {
            if (id <= 0 || name == null || description == null)
            {
                return null;
            }

            var findCourse = _context.Courses.FirstOrDefault(x => x.CourseId == id);

            findCourse.Name = name;
            findCourse.Description = description;

            _context.Update(findCourse);
            _context.SaveChanges();
            return findCourse;
        }
    }
}
