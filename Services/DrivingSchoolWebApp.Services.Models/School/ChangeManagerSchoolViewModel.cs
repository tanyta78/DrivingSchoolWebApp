namespace DrivingSchoolWebApp.Services.Models.School
{
    using AutoMapper;
    using Data.Models;
    using Mapping;

    public class ChangeManagerSchoolViewModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public string ManagerId { get; set; }

        [IgnoreMap]
        public string NewManagerId { get; set; }

       
    }
}
