using System.Collections.Generic;
using SkillsCub.Core;
using SkillsCub.DataLibrary.Entities.Implementation;

namespace SkillsCub.MVC.ViewModels
{
    public class StudentExerciseDetailsViewModel
    {
        public Exercise Exercise { get; set; }

        public IEnumerable<AttachedFile> AttachedFiles { get; set; }
    }
}