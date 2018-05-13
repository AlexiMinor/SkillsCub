using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    public class Exercise
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
    }
}