namespace DrivingSchoolWebApp.Services.Models.Order
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class DetailsOrderViewModel:IMapFrom<Order>
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerUserFirstName { get; set; }

        public string CustomerUserLastName { get; set; }

        [IgnoreMap]
        public string CustomerFullName => this.CustomerUserFirstName + this.CustomerUserLastName;

        public int CourseId { get; set; }

        public string CourseSchoolTradeMark { get; set; }

        public string CourseCategory { get; set; }

        public string CourseDescription { get; set; }

        public decimal CoursePrice { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderedOn { get; set; }

        public decimal PaymentsAmountSum { get; set; }
    }
}
