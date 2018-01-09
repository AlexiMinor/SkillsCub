using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Context;

namespace SkillsCub.DataLibrary.Repositories.Implementation
{
    public class RequestRepository : Repository<Request>
    {
        public RequestRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}