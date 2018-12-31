namespace DrivingSchoolWebApp.Services.Models.Car
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CarViewModel : IMapFrom<Car>
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public Transmission Transmission { get; set; }

        public School Owner { get; set; }

        public string ImageUrl { get; set; }

        public string Title => this.Model +" "+ this.Make +" "+ this.Transmission.ToString();

        public string OwnerTradeMark { get; set; }

        public string OwnerPhone{ get; set; }

    }
}
