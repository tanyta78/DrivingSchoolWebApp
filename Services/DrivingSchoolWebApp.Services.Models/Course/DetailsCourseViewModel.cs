namespace DrivingSchoolWebApp.Services.Models.Course
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class DetailsCourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        //public double Rating { get; set; }
        
        public int MinimumLessonsCount { get; set; }

        public string TrainerNickName { get; set; }

        public string CarImageUrl { get; set; }

        public string CarMake { get; set; }

        public string CarCarModel { get; set; }

        public string CarTransmission { get; set; }

        public string CarVIN { get; set; }

        public int SchoolId { get; set; }

        public string SchoolTradeMark { get; set; }

        public string StudentsCount { get; set; }

        public string AllFeedbacksCount { get; set; }

        public string ExamsTakenCount { get; set; }

    }
}
