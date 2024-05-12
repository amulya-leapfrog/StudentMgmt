using StudentMgmt.Models;

namespace StudentMgmt.Data.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAll();
    }
}
