namespace DrivingSchoolWebApp.Services.Models.Customer
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CreateCustomerInputModel : IMapFrom<Customer>, IMapTo<Customer>
    {
        public string UserId { get; set; }

        public Gender Gender { get; set; }

        public AgeGroup AgeGroup { get; set; }

        public EducationLevel EducationLevel { get; set; }
    }
}
