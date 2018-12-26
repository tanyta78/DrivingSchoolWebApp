namespace DrivingSchoolWebApp.Services.Models.School
{
    using Data.Models;
    using Mapping;

    public class DeleteSchoolViewModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public string ManagerUserName { get; set; }

        public string TradeMark { get; set; }

        public string OfficeAddress { get; set; }

        public string Phone { get; set; }
    }
}