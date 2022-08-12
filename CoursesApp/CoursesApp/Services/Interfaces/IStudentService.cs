using CoursesApp.Models.Dtos;
using System.Collections.Generic;

namespace CoursesApp.Services.Interfaces
{
    public interface IStudentService
    {
        public StudentDto CreateStudent(string name, string lastName, int groupId);
        public IList<StudentDto> GetStudentByGroupId(int groupId);
        public IList<StudentDto> GetStudentList();
        public void RemoveStudent(int studentId);
        public StudentDto UpdateStudent(int studentId, string name, string lastName);
    }
}
