namespace DrivingSchoolWebApp.Services.Models.Customer
{
    using System.Collections.Generic;
    using Data.Models;
    using Mapping;

    public class CustomerViewModel:IMapFrom<Customer>
    {
        //todo make changes to model
        public AppUser User { get; set; }

        public IEnumerable<Order> CoursesOrdered { get; set; } = new HashSet<Order>();

        public IEnumerable<Lesson> LessonsTaken { get; set; } = new HashSet<Lesson>();

        public IEnumerable<Exam> ExamsTaken { get; set; } = new HashSet<Exam>();

        public IEnumerable<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
    }
}
