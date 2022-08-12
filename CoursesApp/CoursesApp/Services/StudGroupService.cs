using System;
using AutoMapper;
using CoursesApp.Models.Dtos;
using CoursesApp.Repository;
using CoursesApp.Services.Interfaces;
using System.Collections.Generic;

namespace CoursesApp.Services
{
    public class StudGroupService : IStudGroupService
    {
        private readonly IStudGroupRepository _studGroupRepository;
        private readonly IMapper _mapper;

        public StudGroupService(IStudGroupRepository studGroupRepository,
            IMapper mapper)
        {
            _studGroupRepository = studGroupRepository ?? throw new ArgumentNullException("Course Repository can't be null", nameof(studGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException("Mapper can't be null", nameof(mapper));
        }

        public StudGroupDto CreateStudGroup(string name, int courseId)
        {
            if (name == null || courseId <= 0)
            {
                return null;
            }
            var savedGroup = _studGroupRepository.CreateStudGroup(courseId, name);
            return _mapper.Map<StudGroupDto>(savedGroup);
        }

        public IList<StudGroupDto> GetStudGroupByCourseId(int courseId)
        {
            if (courseId <= 0)
            {
                return null;
            }
            var studGroup = _studGroupRepository.GetStudGroupByCourseId(courseId);

            var studGroupDto = _mapper.Map<IList<StudGroupDto>>(studGroup);

            return studGroupDto;
        }

        public IList<StudGroupDto> GetStudGroupList()
        {
            var groups = _studGroupRepository.GetStudGroupsList();
            return _mapper.Map<IList<StudGroupDto>>(groups);
        }

        public void RemoveStudGroup(int groupId)
        {
            if (groupId <= 0)
            {
                throw new ArgumentException("GroupId can't be less or equal 0", nameof(groupId));
            }
            _studGroupRepository.RemoveStudGroup(groupId);
        }

        public StudGroupDto UpdateStudGroup(int groupId, string name)
        {
            if (name == null || groupId <= 0)
            {
                return null;
            }
            var saverdGroup = _studGroupRepository.UpdateStudGroup(groupId, name);
            return _mapper.Map<StudGroupDto>(saverdGroup);
        }
    }
}
