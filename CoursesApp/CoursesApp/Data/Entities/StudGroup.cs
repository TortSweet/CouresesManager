using System.Collections.ObjectModel;
using System.Linq;

namespace CoursesApp.Data.Entities
{
    public class StudGroup
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public virtual Collection<Student> Student { get; set; }
    }
}