namespace DrivingSchoolWebApp.Services.Models.Account
{
    using System.Collections.Generic;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class AdminPanelUsersViewModel : IMapFrom<AppUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public UserType UserType { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsApproved { get; set; }

        public List<string> Role { get; set; } = new List<string>();

        public string RolesAsString => string.Join(", ", this.Role);

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap<AppUser, AdminPanelUsersViewModel>()
            //             .ForMember(x => x.Role, m => m.MapFrom(s => s.Roles.Select(r => r.RoleId)));

        }
    }
}
