using System;
using AutoMapper;
using CoursesApp.Models.Dtos;
using CoursesApp.Repository;
using CoursesApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CoursesApp.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository,
            IMapper mapper)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException("Course Repository can't be null", nameof(courseRepository));
            _mapper = mapper ?? throw new ArgumentNullException("Mapper can't be null", nameof(mapper));
        }
        public CourseDto CreateCourse(string name, string description)
        {
            if (name == null || description == null)
            {
                return null;
            }
            var savedCourse = _courseRepository.CreateCourse(name, description);
            return _mapper.Map<CourseDto>(savedCourse);
        }

        public List<CourseDto> GetCoursesList()
        {
            var course = _courseRepository.GetCoursesList();
            
            var courseDtos = _mapper.Map<List<CourseDto>>(course).AsQueryable().ToList();

            return courseDtos;
        }

        public void RemoveCourse(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id can't be less or equal than 0", nameof(id));
            }
            _courseRepository.RemoveCourse(id);
        }

        public CourseDto UpdateCourse(int id, string name, string description)
        {
            if (id <= 0 || name == null || description == null)
            {
                return null;
            }
            var saverdCourse = _courseRepository.UpdateCourse(id, name, description);
            return _mapper.Map<CourseDto>(saverdCourse);
        }
    }
}
