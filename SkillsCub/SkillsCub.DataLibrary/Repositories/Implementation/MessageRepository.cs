using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Context;

namespace SkillsCub.DataLibrary.Repositories.Implementation
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
