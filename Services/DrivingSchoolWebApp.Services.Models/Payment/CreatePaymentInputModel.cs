namespace DrivingSchoolWebApp.Services.Models.Payment
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CreatePaymentInputModel : IMapFrom<Payment>
    {
        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}
