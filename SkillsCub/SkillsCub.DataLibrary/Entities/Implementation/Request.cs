using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    /// <summary>
    /// The request.
    /// </summary>
    [Table("Requests")]
    public class Request : IBaseEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the patronymic.
        /// </summary>
        [Required]
        [Display(Name = "Patronymic")]
        public string Patronymic { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the aplication data.
        /// </summary>
        public DateTime AppliedDate { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the course.
        /// </summary>
        public CourseType Course { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public Source Source { get; set; }

        /// <summary>
        /// Gets or sets the firs time.
        /// </summary>
        public bool FirstTime { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public Status Status { get; set; }

    }
}