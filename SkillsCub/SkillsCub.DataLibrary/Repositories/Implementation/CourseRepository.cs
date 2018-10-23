using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Context;

namespace SkillsCub.DataLibrary.Repositories.Implementation
{
    /// <summary>
    /// The course repository.
    /// </summary>
    public class CourseRepository : Repository<Course>
    {
        /// <summary>
        /// The course repository designer.
        /// </summary>
        /// <param name="context">The aplication database context.</param>
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}