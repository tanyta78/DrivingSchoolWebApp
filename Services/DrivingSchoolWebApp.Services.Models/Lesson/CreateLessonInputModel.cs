namespace DrivingSchoolWebApp.Services.Models.Lesson
{
    using System;
    using Data.Models;
    using Mapping;

    public class CreateLessonInputModel : IMapFrom<Lesson>
    {
        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime StartTime { get; set; }

        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }

        public string Description { get; set; }

    }
}