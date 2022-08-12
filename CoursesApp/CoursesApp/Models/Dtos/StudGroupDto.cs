using System.Collections.Generic;
using System.ComponentModel;
using CoursesApp.Data.Entities;

namespace CoursesApp.Models.Dtos
{
    public class StudGroupDto
    {
        [DisplayName("Group id")]
        public int GroupId { get; set; }
        [DisplayName("Group name")]
        public string Name { get; set; }
        [DisplayName("Course id")]
        public int CourseId { get; set; }
        [DisplayName("Course")]
        public Course Course { get; set; }
        public IList<Student> Student { get; set; }
    }
}
