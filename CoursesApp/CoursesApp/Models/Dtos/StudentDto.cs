using System.ComponentModel;
using CoursesApp.Data.Entities;

namespace CoursesApp.Models.Dtos
{
    public class StudentDto
    {
        [DisplayName("Student id")]
        public int StudentId { get; set; }
        [DisplayName("Group id")]
        public int GroupId { get; set; }
        [DisplayName("Student name")]
        public string Name { get; set; }
        [DisplayName("Student lastname")]
        public string LastName { get; set; }
        public StudGroup StudGroup { get; set; }
    }
}
