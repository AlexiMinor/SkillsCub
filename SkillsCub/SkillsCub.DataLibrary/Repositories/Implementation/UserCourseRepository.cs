using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Context;

namespace SkillsCub.DataLibrary.Repositories.Implementation
{
    public class UserCourseRepository : Repository<UserCourse>
    {
        public UserCourseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}