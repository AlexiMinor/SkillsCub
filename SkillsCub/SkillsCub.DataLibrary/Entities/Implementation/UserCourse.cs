using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    public class UserCourse
    {
        [Key]
        public Guid ID { get; set; }

        public DateTime AssignationDate {get;set;}

        public string StudentID { get; set; }
        [ForeignKey("StudentID")]
        public ApplicationUser Student { get; set; }

        public Guid CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }

    }
}