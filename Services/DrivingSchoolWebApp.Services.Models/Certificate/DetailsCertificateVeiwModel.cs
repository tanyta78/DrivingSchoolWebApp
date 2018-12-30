namespace DrivingSchoolWebApp.Services.Models.Certificate
{
    using System;
    using Data.Models;
    using Mapping;

    public class DetailsCertificateViewModel : IMapFrom<Certificate>
    {

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerFullName { get; set; }

        public int CourseId { get; set; }

        public string CourseInfo { get; set; }

        public DateTime IssueDate { get; set; }

    }
}
