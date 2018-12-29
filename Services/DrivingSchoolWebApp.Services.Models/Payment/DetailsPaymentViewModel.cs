namespace DrivingSchoolWebApp.Services.Models.Payment
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class DetailsPaymentViewModel : IMapFrom<Payment>
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus OrderOrderStatus { get; set; }

        public int OrderCustomerId { get; set; }

        public string OrderCustomerFullName { get; set; }

        public string OrderCourseSchoolTradeMark { get; set; }
    }
}
