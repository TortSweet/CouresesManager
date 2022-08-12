using CoursesApp.Models.Dtos;
using System.Collections.Generic;

namespace CoursesApp.Services.Interfaces
{
    public interface IStudGroupService
    {
        public StudGroupDto CreateStudGroup(string name, int courseId);
        public IList<StudGroupDto> GetStudGroupByCourseId(int courseId);
        public IList<StudGroupDto> GetStudGroupList();
        public void RemoveStudGroup(int groupId);
        public StudGroupDto UpdateStudGroup(int groupId, string name);
    }
}
