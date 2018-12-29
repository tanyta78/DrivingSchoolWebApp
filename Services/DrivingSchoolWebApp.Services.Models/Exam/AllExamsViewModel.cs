namespace DrivingSchoolWebApp.Services.Models.Exam
{
    using System;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class AllExamsViewModel : IMapFrom<Exam>
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerFullName { get; set; }

        public int CourseId { get; set; }

        public string CourseInfo { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public LessonStatus Status { get; set; }

        public ExamType Type { get; set; }
    }
}
