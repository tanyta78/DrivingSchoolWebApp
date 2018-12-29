namespace DrivingSchoolWebApp.Services.Models.Order
{
    using System;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class DetailsOrderViewModel:IMapFrom<Order>
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerFullName { get; set; }

        public int CourseId { get; set; }

        public string CourseSchoolTradeMark { get; set; }

        public string CourseCategory { get; set; }

        public string CourseDescription { get; set; }

        public string CoursePrice { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderedOn { get; set; }

        public decimal PaymentsMade { get; set; }
    }
}
