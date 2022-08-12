using CoursesApp.Data.Entities;
using System.Linq;

namespace CoursesApp.Repository
{
    public interface IStudGroupRepository
    {
        public StudGroup CreateStudGroup(int courseId, string name);
        public IQueryable<StudGroup> GetStudGroupByCourseId(int courseId);
        public IQueryable<StudGroup> GetStudGroupsList();
        public void RemoveStudGroup(int groupId);
        public StudGroup UpdateStudGroup(int groupId, string name);
    }
}
