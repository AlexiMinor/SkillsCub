using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    /// <summary>
    /// The exercise.
    /// </summary>
    public class Exercise : IBaseEntity
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
        /// Gets or setss the condition of problem.
        /// </summary>
        public string ConditionOfProblem { get; set; }

        /// <summary>
        /// Gets or sets the open data time.
        /// </summary>
        public DateTime OpenDateTime { get; set; }

        /// <summary>
        /// Gets or sets the close data time.
        /// </summary>
        public DateTime CloseDateTime { get; set; }

        /// <summary>
        /// Gets or sets the creation data.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the last edit data.
        /// </summary>
        public DateTime? LastEditDate { get; set; }

        /// <summary>
        /// Gets or sets the course id .
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Gets or sets the course.
        /// </summary>
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets the answer value.
        /// </summary>
        public string AnswerValue { get; set; }


        /// <summary>
        /// Gets or sets the mark.
        /// </summary>
        public int Mark { get; set; }

        /// <summary>
        /// Gets or sets the mark comment.
        /// </summary>
        public string MarkComment { get; set; }


        /// <summary>
        /// Gets or sets the answer data time.
        /// </summary>
        public DateTime AnswerDateTime { get; set; }

        /// <summary>
        /// Gets or sets the mark data time.
        /// </summary>
        public DateTime MarkDateTime { get; set; }
    }
}