namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Enums;

    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.IsCourseCompleted = false;
            this.OrderStatus = OrderStatus.AwaitingPayment;
            this.OrderedOn = DateTime.UtcNow;
            this.ActualPriceWhenOrder = this.Course.Price;
        }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public decimal ActualPriceWhenOrder { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderedOn { get; set; }

        public virtual IEnumerable<Payment> Payments { get; set; } = new HashSet<Payment>();

        public bool IsCourseCompleted { get; set; }
    }
}