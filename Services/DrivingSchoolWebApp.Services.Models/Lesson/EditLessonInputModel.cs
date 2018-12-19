namespace DrivingSchoolWebApp.Services.Models.Lesson
{
    using System;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class EditLessonInputModel:IMapFrom<Lesson>
    {
        public int Id { get; set; }

        public LessonStatus Status { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime StartTime { get; set; }

        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }

        public string Description { get; set; }

    }
}