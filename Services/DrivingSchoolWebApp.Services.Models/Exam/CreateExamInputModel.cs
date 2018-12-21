namespace DrivingSchoolWebApp.Services.Models.Exam
{
    using System;
    using Data.Models;
    using Data.Models.Enums;

    public class CreateExamInputModel
    {
        public int CustomerId { get; set; }
        
        public int CourseId { get; set; }
        
        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public LessonStatus Status { get; set; }

        public ExamType Type { get; set; }
    }
}
