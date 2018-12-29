namespace DrivingSchoolWebApp.Services.Models.Lesson
{
    using System;
    using Data.Models;
    using Mapping;

    public class CreateLessonInputModel : IMapFrom<Lesson>, IMapFrom<FullCalendarInputModel>, IMapTo<Lesson>
    {
        public int OrderId { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime StartTime { get; set; }

        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }

        public string Description { get; set; }

        public string Subject { get; set; }
    }
}