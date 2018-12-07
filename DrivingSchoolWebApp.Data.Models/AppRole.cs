namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class AppRole : IdentityRole
    {
        public AppRole()
            : this(null)
        {
        }

        public AppRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

    }
}
