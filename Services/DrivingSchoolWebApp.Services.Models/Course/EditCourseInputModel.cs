namespace DrivingSchoolWebApp.Services.Models.Course
{
    using AutoMapper;
    using Data.Models;
    using Mapping;

    public class EditCourseInputModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int MinimumLessonsCount { get; set; }

        public int TrainerId { get; set; }

        public int CarId { get; set; }

        [IgnoreMap]
        public string Username { get; set; }
    }
}
