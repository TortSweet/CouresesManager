using CoursesApp.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace CoursesApp.Models.Dtos
{
    public class CourseDto
    {
        [DisplayName("Course id")]
        public int CourseId { get; set; }

        [DisplayName("Course name")]
        public string Name { get; set; }

        [DisplayName("Course description")]
        public string Description { get; set; }
        [DisplayName("Groups into course")]
        public IList<StudGroup> StudGroup { get; set; }
    }
}
