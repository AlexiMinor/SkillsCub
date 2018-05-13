using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    public class Course
    {

        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ConsultationDate { get; set; }
        public string ConsultationPlace { get; set; }
        public DateTime StartDate { get; set; }
        public CourseType Type { get; set; }
        public bool IsActive { get; set; }


        public DateTime AssignationDate { get; set; }

        public string TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public ApplicationUser Teacher { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public ApplicationUser Student { get; set; }

        public IEnumerable<Exercise> Exercises { get; set; }


    }
}