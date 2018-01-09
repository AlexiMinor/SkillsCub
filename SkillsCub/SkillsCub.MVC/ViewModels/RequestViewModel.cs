using System.ComponentModel.DataAnnotations;
using SkillsCub.DataLibrary;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.MVC.Models;

namespace SkillsCub.MVC.ViewModels
{
    public class RequestViewModel
    {
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

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public CourseName Course { get; set; }

        public Source Source { get; set; }

        public bool FirstTime { get; set; }

        public bool IsAgreeWithLicense { get; set; }
    }
}