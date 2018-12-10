namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Enums;
    using Microsoft.AspNetCore.Identity;

    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsEnabled = true;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public UserType UserType { get; set; }

        public bool IsEnabled { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; } = new HashSet<IdentityUserRole<string>>();

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = new HashSet<IdentityUserClaim<string>>();

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; } = new HashSet<IdentityUserLogin<string>>();


    }
}
