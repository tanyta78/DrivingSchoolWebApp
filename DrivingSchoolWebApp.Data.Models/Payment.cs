namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using Common;
    using Enums;

    public class Payment:BaseModel<int>
    {
        public Order Order { get; set; }

        public DateTime PaidOn { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}