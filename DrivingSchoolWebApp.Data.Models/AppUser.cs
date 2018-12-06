namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using Enums;
    using Microsoft.AspNetCore.Identity;

    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public UserType UserType { get; set; }

    }
}
