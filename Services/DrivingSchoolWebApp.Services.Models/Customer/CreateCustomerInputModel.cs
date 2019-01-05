namespace DrivingSchoolWebApp.Services.Models.Customer
{
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CreateCustomerInputModel : IMapFrom<Customer>,IHaveCustomMappings
    {
        public string UserId { get; set; }

        [RequiredEnum]
        public Gender Gender { get; set; }

        [RequiredEnum]
        public AgeGroup AgeGroup { get; set; }

        [RequiredEnum]
        public EducationLevel EducationLevel { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateCustomerInputModel, Customer>()
                .ForMember(dest => dest.Id, x => x.Ignore())
                .ForMember(dest => dest.CoursesOrdered, x => x.Ignore())
                .ForMember(dest => dest.ExamsTaken, x => x.Ignore())
                .ForMember(dest => dest.Feedbacks, x => x.Ignore())
                .ForMember(dest => dest.User, x => x.Ignore());

        }
    }
}
