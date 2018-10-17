using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    /// <summary>
    /// The course.
    /// </summary>
    public class Course : IBaseEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the consultation dsta.
        /// </summary>
        public DateTime ConsultationDate { get; set; }

        /// <summary>
        /// Gets or sets consultation place.
        /// </summary>
        public string ConsultationPlace { get; set; }

        /// <summary>
        /// Gets or sets the start data.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the type. 
        /// </summary>
        public CourseType Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the asignation data.
        /// </summary>
        public DateTime AssignationDate { get; set; }

        /// <summary>
        /// Gets or sets the teacher id.
        /// </summary>
        public string TeacherId { get; set; }

        /// <summary>
        /// Gets or sets the teacher.
        /// </summary>
        [ForeignKey("TeacherId")]
        public ApplicationUser Teacher { get; set; }

        /// <summary>
        /// Gets or sets stude3nt id.
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// Gets or sets the student.
        /// </summary>
        [ForeignKey("StudentId")]
        public ApplicationUser Student { get; set; }

        /// <summary>
        /// Gets or sets the exercises.
        /// </summary>
        public IEnumerable<Exercise> Exercises { get; set; }


    }
}