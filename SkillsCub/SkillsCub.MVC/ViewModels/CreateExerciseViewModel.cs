using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SkillsCub.Core;
using SkillsCub.DataLibrary.Entities.Implementation;

namespace SkillsCub.MVC.ViewModels
{
    public class CreateExerciseViewModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public DateTime TimeToOpen { get; set; }
        public DateTime TimeToClose { get; set; }
        public string Detail { get; set; }
        public IEnumerable<AttachedFile> AttachedFiles { get; set; }
        public List<IFormFile> Files { get; set; }

        public List<Exercise> ExercisesForImport { get; set; }
    }
}
  