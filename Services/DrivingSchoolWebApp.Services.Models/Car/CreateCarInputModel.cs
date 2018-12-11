namespace DrivingSchoolWebApp.Services.Models.Car
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class CreateCarInputModel : IMapFrom<Car>, IMapTo<Car>
    {
        public string Model { get; set; }

        public string Make { get; set; }

        public Transmission Transmission { get; set; }

        public School Owner { get; set; }

        public byte[] Image { get; set; }

        public bool InUse { get; set; } = true;

        public string VIN { get; set; }
    }
}
