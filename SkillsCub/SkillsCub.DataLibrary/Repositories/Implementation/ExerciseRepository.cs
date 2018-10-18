using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Context;

namespace SkillsCub.DataLibrary.Repositories.Implementation
{
    /// <summary>
    /// The exercise repository.
    /// </summary>
    public class ExerciseRepository : Repository<Exercise>
    {
        /// <summary>
        /// The exercise repository designer.
        /// </summary>
        /// <param name="context"></param>
        public ExerciseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}