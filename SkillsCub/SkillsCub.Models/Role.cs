using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace SkillsCub.Models
{
    public class Role
    {
        [Key, Identity]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [UpdatedAt]
        public DateTime UpdatedAt { get; set; }
    }
}