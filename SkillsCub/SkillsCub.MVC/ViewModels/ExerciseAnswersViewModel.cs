using System.Collections.Generic;
using SkillsCub.DataLibrary.Entities.Implementation;

namespace SkillsCub.MVC.ViewModels
{
    public class ExerciseAnswersViewModel
    {
        public Exercise Exercise { get; set; }

        public IEnumerable<Answer> Answers { get; set; }
    }
}
