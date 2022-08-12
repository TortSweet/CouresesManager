using AutoMapper;
using CoursesApp.Data.Entities;
using CoursesApp.Models.Dtos;

namespace CoursesApp.MapperProfile
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentDto>();
        }
    }
}
