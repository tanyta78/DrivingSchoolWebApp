namespace DrivingSchoolWebApp.Services.Models.Feedback
{
    public class CreateFeedbackInputModel
    {
        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}
