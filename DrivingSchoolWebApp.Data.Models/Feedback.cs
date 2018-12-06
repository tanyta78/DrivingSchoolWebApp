namespace DrivingSchoolWebApp.Data.Models
{
    using Common;

    public class Feedback : BaseModel<int>
    {
        public Customer Customer { get; set; }

        public Course Course { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}