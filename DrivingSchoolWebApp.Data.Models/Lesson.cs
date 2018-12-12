namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using Common;
    using Enums;

    public class Lesson : BaseModel<int>
    {
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public LessonStatus Status { get; set; }

        public DateTime DateOfLesson { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }
    }
}