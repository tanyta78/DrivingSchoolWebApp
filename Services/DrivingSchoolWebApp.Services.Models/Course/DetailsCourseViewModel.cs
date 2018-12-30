namespace DrivingSchoolWebApp.Services.Models.Course
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class DetailsCourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        [IgnoreMap]
        public double Rating { get; set; }
        
        public int MinimumLessonsCount { get; set; }

        public string TrainerUserNickName { get; set; }

        public string CarImageUrl { get; set; }

        public string CarMake { get; set; }

        public string CarCarModel { get; set; }

        public string CarTransmission { get; set; }

        public string CarVIN { get; set; }

        public int SchoolId { get; set; }

        public string SchoolTradeMark { get; set; }

        public int StudentsCount { get; set; }

        public int AllFeedbacksCount { get; set; }

        public int ExamsTakenCount { get; set; }

        public int AllFeedbacksRaitingAverage { get; set; }

        
        }
}
