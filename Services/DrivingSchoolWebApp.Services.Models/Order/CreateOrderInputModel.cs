namespace DrivingSchoolWebApp.Services.Models.Order
{
    using AutoMapper;
    using Data.Models;
    using Mapping;

    public class CreateOrderInputModel : IMapFrom<Order>, IMapTo<Order>,IHaveCustomMappings
    {
        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        public decimal ActualPriceWhenOrder { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateOrderInputModel, Order>()
                .ForMember(dest => dest.Id, x => x.Ignore())
                .ForMember(dest => dest.Customer, x => x.Ignore())
                .ForMember(dest => dest.IsCourseCompleted, x => x.Ignore())
                .ForMember(dest => dest.IsFullyPaid, x => x.Ignore())
                .ForMember(dest => dest.LessonsTaken, x => x.Ignore())
                .ForMember(dest => dest.OrderStatus, x => x.Ignore())
                .ForMember(dest => dest.OrderedOn, x => x.Ignore())
                .ForMember(dest => dest.Payments, x => x.Ignore())
                .ForMember(dest => dest.Course, x => x.Ignore());
        }
    }
}
