namespace DrivingSchoolWebApp.Services.Models.Certificate
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateCertificateInputModel
    {
        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }
    }
}
