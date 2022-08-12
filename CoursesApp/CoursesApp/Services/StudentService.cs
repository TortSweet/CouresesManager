using System;
using AutoMapper;
using CoursesApp.Models.Dtos;
using CoursesApp.Repository;
using CoursesApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace CoursesApp.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository ?? throw new ArgumentNullException("Course Repository can't be null", nameof(studentRepository));
            _mapper = mapper ?? throw new ArgumentNullException("Mapper can't be null", nameof(mapper));
        }
        public StudentDto CreateStudent(string name, string lastName, int groupId)
        {
            if (name == null || lastName == null || groupId <= 0)
            {
                return null;
            }
            var savedStudent = _studentRepository.CreateStudent(groupId, name, lastName);
            return _mapper.Map<StudentDto>(savedStudent);
        }

        public IList<StudentDto> GetStudentByGroupId(int groupId)
        {
            if (groupId <= 0)
            {
                return null;
            }
            var students = _studentRepository.GetStudentByGroupId(groupId);

            var studentsDto = _mapper.Map<List<StudentDto>>(students);

            return studentsDto;
        }

        public IList<StudentDto> GetStudentList()
        {
            var student = _studentRepository.GetStudentList().ToList();
            return _mapper.Map<List<StudentDto>>(student);
        }

        public void RemoveStudent(int studentId)
        {
            if (studentId <= 0)
            {
                throw  new ArgumentException("Id can't be less or equal 0", nameof(studentId));
            }
            _studentRepository.RemoveStudent(studentId);
        }

        public StudentDto UpdateStudent(int studentId, string name, string lastName) 
        {
            if (name == null || lastName == null || studentId < 0)
            {
                return null;
            }
            var savedStudent = _studentRepository.UpdateStudent(studentId, name, lastName);
            return _mapper.Map<StudentDto>(savedStudent);
        }
    }
}
