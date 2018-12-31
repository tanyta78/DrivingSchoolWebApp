namespace DrivingSchoolWebApp.Services.Models.Feedback
{
    using AutoMapper;
    using Data.Models;
    using Mapping;

    public class AllFeedbackViewModel:IMapFrom<Feedback>,IHaveCustomMappings
    {
        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        public string CustomerFullName { get; set; }

        public string CourseDescription { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public void CreateMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Feedback, AllFeedbackViewModel>()
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer.User.FirstName + " " + src.Customer.User.LastName));
        }
    }
}
