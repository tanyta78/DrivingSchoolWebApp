namespace DrivingSchoolWebApp.Services.Models.School
{
    using Data.Models;
    using Mapping;

    public class CreateSchoolInputModel : IMapFrom<School>
    {
        public string ManagerId { get; set; }

        public AppUser Manager { get; set; }

        public string TradeMark { get; set; }

        public string Phone { get; set; }
        
        public string OfficeAddress { get; set; }

        public bool IsActive { get; set; }

    }
}