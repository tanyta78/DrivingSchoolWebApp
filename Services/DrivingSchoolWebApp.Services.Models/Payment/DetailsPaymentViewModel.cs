namespace DrivingSchoolWebApp.Services.Models.Payment
{
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class DetailsPaymentViewModel : IMapFrom<Payment>
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus OrderOrderStatus { get; set; }

        public string OrderCourseSchoolTradeMark { get; set; }

        public string OrderCustomerUserFirstName { get; set; }

        public string OrderCustomerUserLastName { get; set; }

        [IgnoreMap]
        public string OrderCustomerFullName => this.OrderCustomerUserFirstName + this.OrderCustomerUserLastName;
    }
}
