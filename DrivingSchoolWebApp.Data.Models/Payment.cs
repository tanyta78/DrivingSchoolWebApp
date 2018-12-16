namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using Common;
    using Enums;

    public class Payment : BaseModel<int>
    {
        public Payment()
        {
            this.PaidOn = DateTime.UtcNow;
        }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public DateTime PaidOn { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}