namespace DrivingSchoolWebApp.Data.Models
{
    using Common;

    public class Feedback : BaseModel<int>
    {
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}