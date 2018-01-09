using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace SkillsCub.Models
{
    public class User
    {
        [Key, Identity]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}