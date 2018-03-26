using System.Collections.Generic;
using SkillsCub.DataLibrary.Entities.Implementation;

namespace SkillsCub.MVC.ViewModels
{
    public class CourseModel
    {
        public Course Course { get; set; }
        public IEnumerable<UserCourse> Students { get; set; }

        public IEnumerable<Exercise> Exercises { get; set; }
    }
}