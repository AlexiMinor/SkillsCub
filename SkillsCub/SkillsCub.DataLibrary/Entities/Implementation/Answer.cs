using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    public class Answer
    {
        public Guid ID { get; set; }

        public string Value { get;set; }

        public Guid CourseTaskID { get; set; }
        [ForeignKey("CourseTaskID")]
        public CourseExcercise CourseExcercise { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User{ get; set; }

    }
}
