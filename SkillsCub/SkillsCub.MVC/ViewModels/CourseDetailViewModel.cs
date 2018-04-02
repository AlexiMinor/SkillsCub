using System.Collections.Generic;
using SkillsCub.DataLibrary.Entities.Implementation;

namespace SkillsCub.MVC.ViewModels
{
    public class CourseDetailViewModel
    {
        public Course Course { get; set; }
        public  IEnumerable<ApplicationUser> Students { get; set; }
    }
}