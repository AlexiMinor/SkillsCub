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

        public int Mark { get; set; }

        public DateTime AnswerDateTime { get; set; }
        public DateTime MarkDateTime { get; set; }
        public Guid ExerciseID { get; set; }
        [ForeignKey("ExerciseID")]
        public Exercise Exercise { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User{ get; set; }

    }
}
