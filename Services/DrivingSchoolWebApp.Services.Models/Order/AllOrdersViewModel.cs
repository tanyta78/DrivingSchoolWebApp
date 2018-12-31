namespace DrivingSchoolWebApp.Services.Models.Order
{
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class AllOrdersViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        public string CourseSchoolTradeMark { get; set; }

        public string CourseCategory { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string CustomerFullName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Order, AllOrdersViewModel>()
              .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer.User.FirstName + " " + src.Customer.User.LastName));
        }
    }
}
