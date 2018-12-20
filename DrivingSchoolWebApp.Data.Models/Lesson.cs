namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using Common;
    using Enums;

    public class Lesson : BaseModel<int>
    {
        private string subject;

        public Lesson()
        {
            this.Status = LessonStatus.Scheduled;
        }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public LessonStatus Status { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime StartTime { get; set; }

        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }

        public string Subject  { get; set; }

        public string Description { get; set; }

    }
}