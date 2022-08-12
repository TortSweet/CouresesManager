namespace CoursesApp.Data.Entities
{
    public class Student
    {
        public int StudentId { get; set; }

        public int GroupId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
        public StudGroup StudGroup { get; set; }
    }
}