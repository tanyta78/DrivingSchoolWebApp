namespace DrivingSchoolWebApp.Services.Models.Certificate
{
    using System;

    public class CreateCertificateInputModel
    {
        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        public DateTime IssueDate { get; set; }
    }
}
