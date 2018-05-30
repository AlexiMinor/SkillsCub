using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    public class Exercise : IBaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ConditionOfProblem { get; set; }

        public DateTime OpenDateTime { get; set; }
        public DateTime CloseDateTime { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? LastEditDate { get; set; }

        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string AnswerValue { get; set; }

        public int Mark { get; set; }
        public string MarkComment { get; set; }

        public DateTime AnswerDateTime { get; set; }
        public DateTime MarkDateTime { get; set; }
    }
}