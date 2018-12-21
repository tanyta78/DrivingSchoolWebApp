namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using Common;

    public class Certificate : BaseModel<int>
    {
        public Certificate()
        {
            this.IssueDate=DateTime.UtcNow;
        }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public DateTime IssueDate { get; set; }
    }
}
