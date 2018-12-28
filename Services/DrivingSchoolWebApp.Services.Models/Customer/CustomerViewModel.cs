namespace DrivingSchoolWebApp.Services.Models.Customer
{
    using System.Collections.Generic;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CustomerViewModel:IMapFrom<Customer>
    {
        public int Id { get; set; }
     
        public AppUser User { get; set; }

        public Gender Gender { get; set; }

        public AgeGroup AgeGroup { get; set; }

        public EducationLevel EducationLevel { get; set; }

        public IEnumerable<Order> CoursesOrdered { get; set; } = new HashSet<Order>();

        public IEnumerable<Lesson> LessonsTaken { get; set; } = new HashSet<Lesson>();

        public IEnumerable<Exam> ExamsTaken { get; set; } = new HashSet<Exam>();

        public IEnumerable<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
    }
}
