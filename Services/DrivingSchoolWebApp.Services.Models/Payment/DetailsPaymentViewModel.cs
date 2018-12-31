namespace DrivingSchoolWebApp.Services.Models.Payment
{
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class DetailsPaymentViewModel : IMapFrom<Payment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus OrderOrderStatus { get; set; }

        public string OrderCourseSchoolTradeMark { get; set; }

        public string OrderCustomerFullName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Payment, DetailsPaymentViewModel>()
               .ForMember(dest => dest.OrderCustomerFullName, opt => opt.MapFrom(src => src.Order.Customer.User.FirstName + " " + src.Order.Customer.User.LastName));
        }
    }
}
