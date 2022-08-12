using System.Collections.ObjectModel;
using System.Linq;

namespace CoursesApp.Data.Entities
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public virtual Collection<StudGroup> StudGroup { get; set; }
    }
}
