using System.Collections.Generic;
using SkillsCub.Core;
using SkillsCub.DataLibrary.Entities.Implementation;

namespace SkillsCub.MVC.ViewModels
{
    public class StudentCourseViewModel
    {
        public  string CourseName { get; set; }
        public Dictionary<Exercise, IEnumerable<AttachedFile>> Exercises { get; set; }
    }
}
