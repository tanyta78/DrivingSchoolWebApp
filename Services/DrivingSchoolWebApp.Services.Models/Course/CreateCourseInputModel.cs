namespace DrivingSchoolWebApp.Services.Models.Course
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CreateCourseInputModel : IMapFrom<Course>
    {
        public Category Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int MinimumLessonsCount { get; set; }

        public int TrainerId { get; set; }

        public int CarId { get; set; }

        public int SchoolId { get; set; }

    }
}
