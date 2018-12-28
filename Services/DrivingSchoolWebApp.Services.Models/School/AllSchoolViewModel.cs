namespace DrivingSchoolWebApp.Services.Models.School
{
    using Data.Models;
    using Mapping;

    public class AllSchoolViewModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public string TradeMark { get; set; }

        public string OfficeAddress { get; set; }

        public string Phone { get; set; }

    }
}