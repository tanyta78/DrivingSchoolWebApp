namespace DrivingSchoolWebApp.Services.Models.Order
{
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class AllOrdersViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerUserFirstName { get; set; }

        public string CustomerUserLastName { get; set; }

        public int CourseId { get; set; }

        public string CourseSchoolTradeMark { get; set; }

        public string CourseCategory { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [IgnoreMap]
        public string CustomerFullName => this.CustomerUserFirstName + this.CustomerUserLastName;

    }
}
