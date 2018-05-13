using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillsCub.DataLibrary.Entities.Implementation;

namespace SkillsCub.MVC.ViewModels
{
    public class CourseViewModel
    {
        public CourseType Type { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime ConsultationDate { get; set; }
        public DateTime ConsultationTime { get; set; }
        public string Place { get; set; }

        public string TeacherId { get; set; }
        public string StudentId { get; set; }

        public IEnumerable<SelectListItem> Teachers { get; set; }
        public IEnumerable<SelectListItem> Students { get; set; }
    }
}