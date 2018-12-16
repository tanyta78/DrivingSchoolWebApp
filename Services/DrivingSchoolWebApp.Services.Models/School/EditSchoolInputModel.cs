namespace DrivingSchoolWebApp.Services.Models.School
{
    using Data.Models;
    using Mapping;

    public class EditSchoolInputModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public AppUser Manager { get; set; }

        public string TradeMark { get; set; }

        public string OfficeAddress { get; set; }

        public bool IsActive { get; set; }
    }
}