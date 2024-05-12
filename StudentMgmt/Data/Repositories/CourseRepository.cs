using StudentMgmt.Data.Interfaces;
using StudentMgmt.Models;
using Microsoft.EntityFrameworkCore;


namespace StudentMgmt.Data.Repositories
{
    public class CourseRepository: ICourseRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CourseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await dbContext.Course.ToListAsync();
        }
    }
}
