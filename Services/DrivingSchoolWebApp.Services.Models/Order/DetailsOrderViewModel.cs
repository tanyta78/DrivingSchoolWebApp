namespace DrivingSchoolWebApp.Services.Models.Order
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class DetailsOrderViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerUserId { get; set; }

        public string CustomerFullName { get; set; }

        public int CourseId { get; set; }

        public int CourseTrainerId { get; set; }

        public string CourseSchoolTradeMark { get; set; }

        public string CourseCategory { get; set; }

        public string CourseDescription { get; set; }

        public decimal CoursePrice { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderedOn { get; set; }

        public decimal PaymentsAmountSum { get; set; }

        [IgnoreMap] public decimal RemainingAmount => this.CoursePrice - this.PaymentsAmountSum;

        public void CreateMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Order, DetailsOrderViewModel>()
                .ForMember(dest => dest.PaymentsAmountSum,
                    opt => opt.MapFrom(src => src.Payments.Select(p => p.Amount).Sum()))
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer.User.FirstName + " " + src.Customer.User.LastName));
        }
    }
}
