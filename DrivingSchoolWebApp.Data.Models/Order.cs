﻿namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Enums;

    public class Order : BaseModel<int>
    {
        public Customer Customer { get; set; }

        public Course Course { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderedOn { get; set; }

        public virtual IEnumerable<Payment> Payments { get; set; } = new HashSet<Payment>();

        public bool IsCourseCompleted { get; set; }
    }
}