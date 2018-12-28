namespace DrivingSchoolWebApp.Services.Models.Customer
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class DetailsCustomerViewModel : IMapFrom<Customer>
    {
        public int Id { get; set; }

        public AppUser User { get; set; }

        public string UserNickName { get; set; }

        public string UserAddress { get; set; }

        public string UserPhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public AgeGroup AgeGroup { get; set; }

        public EducationLevel EducationLevel { get; set; }

    }
}
