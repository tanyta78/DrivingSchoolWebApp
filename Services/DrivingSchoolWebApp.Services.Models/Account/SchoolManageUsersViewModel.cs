namespace DrivingSchoolWebApp.Services.Models.Account
{
    using Data.Models;
    using Mapping;

    public class SchoolManageUsersViewModel : IMapFrom<AppUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

    }
}
