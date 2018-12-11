namespace DrivingSchoolWebApp.Services.Models.Car
{
    using Data.Models;
    using Data.Models.Enums;

    public class CarDetailsViewModel
    {
        public string Model { get; set; }

        public string Make { get; set; }

        public Transmission Transmission { get; set; }

        public School Owner { get; set; }

        public byte[] Image { get; set; }

       
    }
}
