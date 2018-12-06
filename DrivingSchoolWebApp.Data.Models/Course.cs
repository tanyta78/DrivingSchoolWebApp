namespace DrivingSchoolWebApp.Data.Models
{
    using System.Collections.Generic;
    using Common;
    using Enums;

    public class Course : BaseModel<int>
    {
        public Category Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int MinimumLessonsCount { get; set; }

        public bool IsFinished { get; set; }

        public double Rating { get; set; }

        public virtual School School { get; set; }

        public virtual Trainer Trainer { get; set; }

        public virtual Car Car { get; set; }

        public virtual IEnumerable<Order> Students { get; set; } = new HashSet<Order>();

        public virtual IEnumerable<Feedback> AllFeedbacks { get; set; } = new HashSet<Feedback>();

        public virtual IEnumerable<Exam> ExamsTaken { get; set; } = new HashSet<Exam>();
    }
}