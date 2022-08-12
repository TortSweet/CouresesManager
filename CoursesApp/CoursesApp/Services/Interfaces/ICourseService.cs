using CoursesApp.Models.Dtos;
using System.Collections.Generic;

namespace CoursesApp.Services.Interfaces
{
    public interface ICourseService
    {
        public CourseDto CreateCourse(string name, string description);
        public List<CourseDto> GetCoursesList();
        public void RemoveCourse(int courseId);
        public CourseDto UpdateCourse(int id, string name, string description);
    }
}
