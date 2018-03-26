﻿using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Context;

namespace SkillsCub.DataLibrary.Repositories.Implementation
{
    public class AnswerRepository : Repository<Answer>
    {
        public AnswerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}