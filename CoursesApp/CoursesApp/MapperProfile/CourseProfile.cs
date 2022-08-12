using AutoMapper;
using CoursesApp.Data.Entities;
using CoursesApp.Models.Dtos;

namespace CoursesApp.MapperProfile
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseDto, Course>();
            CreateMap<Course, CourseDto>();
        }
    }
}
