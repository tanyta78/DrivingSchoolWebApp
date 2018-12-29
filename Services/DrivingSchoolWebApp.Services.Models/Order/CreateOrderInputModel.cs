namespace DrivingSchoolWebApp.Services.Models.Order
{
    using Data.Models;
    using Mapping;

    public class CreateOrderInputModel : IMapFrom<Order>
    {
        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        public decimal ActualPriceWhenOrder { get; set; }

    }
}
