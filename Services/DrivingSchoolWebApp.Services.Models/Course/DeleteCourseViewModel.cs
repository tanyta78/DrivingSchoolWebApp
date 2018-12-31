namespace DrivingSchoolWebApp.Services.Models.Course
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class DeleteCourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public Category Category { get; set; }

        public string TrainerUserNickName { get; set; }

        public string CarVIN { get; set; }

        public string SchoolTradeMark { get; set; }

    }
}
