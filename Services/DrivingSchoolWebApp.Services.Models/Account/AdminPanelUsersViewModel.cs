namespace DrivingSchoolWebApp.Services.Models.Account
{
    using System.Collections.Generic;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class AdminPanelUsersViewModel:IMapFrom<AppUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public UserType UserType { get; set; }

        public bool IsEnabled { get; set; }

        public List<string> Role { get; set; } = new List<string>();

        public string RolesAsString => string.Join(", ", this.Role);
    }
}
