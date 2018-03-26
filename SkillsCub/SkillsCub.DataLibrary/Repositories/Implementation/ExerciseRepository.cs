using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Context;

namespace SkillsCub.DataLibrary.Repositories.Implementation
{
    public class ExerciseRepository : Repository<Exercise>
    {
        public ExerciseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}