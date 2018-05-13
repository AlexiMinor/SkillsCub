using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    public class Answer
    {
        public Guid Id { get; set; }

        public string Value { get;set; }

        public int Mark { get; set; }
        public string MarkComment { get; set; }

        public DateTime AnswerDateTime { get; set; }
        public DateTime MarkDateTime { get; set; }
        public Guid ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User{ get; set; }

    }
}
