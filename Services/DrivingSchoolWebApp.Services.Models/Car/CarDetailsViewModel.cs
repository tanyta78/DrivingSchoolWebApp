namespace DrivingSchoolWebApp.Services.Models.Car
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CarDetailsViewModel:IMapFrom<Car>
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public Transmission Transmission { get; set; }

        public School Owner { get; set; }

        public string ImageUrl { get; set; }

       
    }
}
