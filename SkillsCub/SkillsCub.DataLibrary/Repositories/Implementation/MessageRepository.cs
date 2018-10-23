using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Context;

namespace SkillsCub.DataLibrary.Repositories.Implementation
{
    /// <summary>
    /// The messege repository.
    /// </summary>
    public class MessageRepository : Repository<Message>
    {
        /// <summary>
        /// The messege repository designer.
        /// </summary>
        /// <param name="context">The aplication database context.</param>
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
