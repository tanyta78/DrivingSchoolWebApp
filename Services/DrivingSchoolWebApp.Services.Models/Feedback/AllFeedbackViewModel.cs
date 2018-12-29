namespace DrivingSchoolWebApp.Services.Models.Feedback
{
    using Data.Models;
    using Mapping;

    public class AllFeedbackViewModel:IMapFrom<Feedback>
    {
        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        public string CustomerFullName { get; set; }

        public string CourseDescription { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}
