using System;

namespace SkilsCub.TokenModels
{
    public class User
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
