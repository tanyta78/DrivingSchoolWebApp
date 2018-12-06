namespace DrivingSchoolWebApp.Data.Models
{
    using System.Collections.Generic;
    using Common;

    public class Customer : BaseModel<int>
    {
        public AppUser User { get; set; }

        public IEnumerable<Order> CoursesOrdered { get; set; } = new HashSet<Order>();

        public IEnumerable<Lesson> LessonsTaken { get; set; } = new HashSet<Lesson>();

        public IEnumerable<Exam> ExamsTaken { get; set; } = new HashSet<Exam>();

        public IEnumerable<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();

    }
}