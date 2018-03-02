using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    public class Course
    {

        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ConsultationDate { get; set; }

        public DateTime StartDate { get; set; }

        public CourseName CourseName { get; set; }
        public bool IsActive { get; set; }


        public string TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public ApplicationUser Teacher { get; set; }

        public IEnumerable<UserCourse> Students { get; set; }

        public IEnumerable<CourseExcercise> CourseExcercises { get; set; }


    }
}