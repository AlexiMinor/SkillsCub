using System.ComponentModel.DataAnnotations;

namespace SkillsCub.MVC.Models.ViewModels
{
    public class RequestViewModel
    {

        [Required]
        [EmailAddress]
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

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public int Course { get; set; }

        public int Source { get; set; }

        public bool FirstTime { get; set; }
    }
}