using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper;
using MicroOrm.Dapper.Repositories.Attributes;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    [Table("Requests")]
    public class Request 
    {
        [Key, Identity]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Patronymic")]
        public string Patronymic { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public DateTime AppliedDate { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public CourseType Course { get; set; }

        public Source Source { get; set; }

        public bool FirstTime { get; set; }

        public Status Status { get; set; }

    }
}