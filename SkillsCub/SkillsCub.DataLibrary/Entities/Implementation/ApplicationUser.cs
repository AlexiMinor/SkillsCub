﻿using System;
using Microsoft.AspNetCore.Identity;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsActive { get; set; }
    }
}
