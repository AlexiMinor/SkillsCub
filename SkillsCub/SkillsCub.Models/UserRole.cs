using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace SkillsCub.Models
{
    public class UserRole
    {
        [Key, Identity]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        [UpdatedAt]
        public DateTime UpdatedAt { get; set; }
    }
}
