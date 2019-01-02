namespace DrivingSchoolWebApp.Services.Models.Order
{
    using AutoMapper;
    using Data.Models;
    using Mapping;

    public class FullCalendarOrdersViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Info { get; set; }

        public void CreateMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Order, FullCalendarOrdersViewModel>()
                .ForMember(dest => dest.Info, opt => opt.MapFrom(src => src.Customer.User.FirstName + " " + src.Customer.User.LastName + " " + src.Course.Trainer.User.Nickname));
        }
    }
}
