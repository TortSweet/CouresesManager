using AutoMapper;
using CoursesApp.Data.Entities;
using CoursesApp.Models.Dtos;

namespace CoursesApp.MapperProfile
{
    public class StudentGroupProfile : Profile
    {
        public StudentGroupProfile()
        {
            CreateMap<StudGroupDto, StudGroup>();
            CreateMap<StudGroup, StudGroupDto>();
        }
    }
}
