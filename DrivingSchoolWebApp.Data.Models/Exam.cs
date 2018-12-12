namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using Common;
    using Enums;

    public class Exam:BaseModel<int>
    {
        public int CustomerId { get; set; }
        
        public virtual Customer Customer { get; set; }

        public int CourseId { get; set; }
        
        public virtual Course Course { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public LessonStatus Status { get; set; }

        public ExamType Type { get; set; }
    }
}