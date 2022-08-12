using CoursesApp.Data.Entities;
using System.Linq;

namespace CoursesApp.Repository
{
    public interface ICourseRepository
    {
        public Course CreateCourse(string name, string description);
        public IQueryable<Course> GetCoursesList();
        public void RemoveCourse(int id);
        public Course UpdateCourse(int id, string name, string description);
    }
}
