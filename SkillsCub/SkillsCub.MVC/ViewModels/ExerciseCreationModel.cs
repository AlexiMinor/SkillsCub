using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsCub.MVC.ViewModels
{
    public class ExerciseModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public DateTime TimeToOpen { get; set; }
        public DateTime TimeToClose { get; set; }
        public string Detail { get; set; }
    }
}