namespace DrivingSchoolWebApp.Data.Models
{
    using System.Collections.Generic;
    using Common;
    using Enums;

    public class Customer : BaseModel<int>
    {
        public string UserId { get; set; }

        public virtual AppUser User { get; set; }

        public Gender Gender { get; set; }

        public AgeGroup AgeGroup { get; set; }

        public EducationLevel EducationLevel { get; set; }

        //public string FullName => this.User.FirstName + " " + this.User.LastName;

        public virtual IEnumerable<Order> CoursesOrdered { get; set; } = new HashSet<Order>();

        public virtual IEnumerable<Exam> ExamsTaken { get; set; } = new HashSet<Exam>();

        public virtual IEnumerable<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();

    }
}