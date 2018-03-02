using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    public class CourseExcercise
    {
        public Guid ID { get; set; }

        public Guid CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }

        public Guid ExcerciseID { get; set; }
        [ForeignKey("ExcerciseID")]
        public Excercise Excercise { get; set; }

        public IEnumerable<Answer> Answers { get; set; }


    }
}