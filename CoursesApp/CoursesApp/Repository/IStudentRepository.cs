using CoursesApp.Data.Entities;
using System.Linq;


namespace CoursesApp.Repository
{
    public interface IStudentRepository
    {
        public Student CreateStudent(int groupId, string name, string lastName);
        public IQueryable<Student> GetStudentByGroupId(int groupId);
        public IQueryable<Student> GetStudentList();
        public void RemoveStudent(int studentId);
        public Student UpdateStudent(int studentId, string name, string lastName);
    }
}
