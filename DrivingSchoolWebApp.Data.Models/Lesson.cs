namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using Common;
    using Enums;

    public class Lesson : BaseModel<int>
    {
        public Customer Customer { get; set; }

        public Course Course { get; set; }

        public LessonStatus Status { get; set; }

        public DateTime DateOfLesson { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }
    }
}