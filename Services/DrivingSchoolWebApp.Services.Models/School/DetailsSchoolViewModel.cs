namespace DrivingSchoolWebApp.Services.Models.School
{
    using Data.Models;
    using Mapping;

    public class DetailsSchoolViewModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public string ManagerNickName { get; set; }

        public string ManagerId { get; set; }
        
        public string TradeMark { get; set; }

        public string OfficeAddress { get; set; }

        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public int CoursesOfferedCount { get; set; }

        public int TrainersCount { get; set; }

        public int OrdersCount { get; set; }

        public int OwnedCarsCount { get; set; }

    }
}