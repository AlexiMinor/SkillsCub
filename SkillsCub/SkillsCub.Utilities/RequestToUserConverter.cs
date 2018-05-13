using System;
using SkillsCub.DataLibrary.Entities.Implementation;

namespace SkillsCub.Utilities
{
    public static class RequestToUserConverter
    {
        public static ApplicationUser ConvertToUser(Request request) => new ApplicationUser
        {
            //this Id'll be send to e-mail
            Id = Guid.NewGuid().ToString("D"),

            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronymic = request.Patronymic,
            Email = request.Email,
            DateCreated = request.AppliedDate,
            PhoneNumber = request.Phone,
            UserName = request.Email,
            IsActive = false,
            LastModified = DateTime.Now
        };
    }
}