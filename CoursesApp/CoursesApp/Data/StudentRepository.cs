using System;
using CoursesApp.Data.Entities;
using CoursesApp.Repository;
using System.Linq;

namespace CoursesApp.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDBContext _context;

        public StudentRepository(AppDBContext context)
        {
            _context = context ?? throw new ArgumentNullException("AppDbContext can't be null", nameof(context));
        }
        public Student CreateStudent(int groupId, string name, string lastName)
        {
            if (groupId <= 0 || name == null || lastName == null)
            {
                return null;
            }
            var newStudent = new Student(){Name = name, LastName = lastName, GroupId = groupId};

            _context.Students.Add(newStudent);
            _context.SaveChanges();

            return newStudent;
        }

        public IQueryable<Student> GetStudentByGroupId(int groupId)
        {
            if (groupId <= 0)
            {
                return null;
            }
            IQueryable<Student> students = (from b in _context.Students where groupId == b.GroupId select b);
            return students;
        }

        public IQueryable<Student> GetStudentList()
        {
            return _context.Students;
        }

        public void RemoveStudent(int studentId)
        {
            if (studentId <= 0)
            {
                throw new ArgumentException("Id can't be less or equal than 0", nameof(studentId));
            }
            var student = _context.Students.FirstOrDefault(x => x.StudentId == studentId);
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public Student UpdateStudent(int studentId, string name, string lastName)
        {
            if (studentId <= 0 || name == null || lastName == null)
            {
                return null;
            }
            var student = _context.Students.FirstOrDefault(e => e.StudentId == studentId);
            student.Name = name;
            student.LastName = lastName;
            _context.SaveChanges();
            return student;
        }
    }
}
