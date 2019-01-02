namespace DrivingSchoolWebApp.Services.Models.Car
{
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CarDetailsViewModel : IMapFrom<Car>
    {
        public int Id { get; set; }

        public string CarModel { get; set; }

        public string Make { get; set; }

        public Transmission Transmission { get; set; }

        public string ImageUrl { get; set; }

        public string OwnerManagerUserId { get; set; }

        public virtual School Owner { get; set; }

        [IgnoreMap]
        public string Username { get; set; }
    }
}
