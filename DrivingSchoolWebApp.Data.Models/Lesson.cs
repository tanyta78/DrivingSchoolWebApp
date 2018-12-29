namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using Common;
    using Enums;

    public class Lesson : BaseModel<int>
    {
       public Lesson()
        {
            this.Status = LessonStatus.Scheduled;
        }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public LessonStatus Status { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime StartTime { get; set; }

        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }

        public string Subject  { get; set; }

        public string Description { get; set; }

    }
}