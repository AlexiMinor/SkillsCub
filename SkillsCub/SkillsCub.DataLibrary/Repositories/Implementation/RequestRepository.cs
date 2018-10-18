using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Context;

namespace SkillsCub.DataLibrary.Repositories.Implementation
{
    /// <summary>
    /// The request repository.
    /// </summary>
    public class RequestRepository : Repository<Request>
    {
        /// <summary>
        /// The request repository designer.
        /// </summary>
        /// <param name="context"></param>
        public RequestRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}