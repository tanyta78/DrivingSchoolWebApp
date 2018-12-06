namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using Common;
    using Enums;

    public class Exam:BaseModel<int>
    {
        public Customer Customer { get; set; }

        public Course Course { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public LessonStatus Status { get; set; }

        public ExamType Type { get; set; }
    }
}