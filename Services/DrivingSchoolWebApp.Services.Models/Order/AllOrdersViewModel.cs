namespace DrivingSchoolWebApp.Services.Models.Order
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class AllOrdersViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerFullName { get; set; }

        public int CourseId { get; set; }

        public string CourseSchoolTradeMark { get; set; }

        public string CourseCategory { get; set; }

        public OrderStatus OrderStatus { get; set; }

    }
}
