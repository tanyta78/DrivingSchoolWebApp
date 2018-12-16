﻿namespace DrivingSchoolWebApp.Services.Models.Car
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;
    using Microsoft.AspNetCore.Http;

    public class CreateCarInputModel : IMapFrom<Car>, IMapTo<Car>
    {
        public string Model { get; set; }

        public string Make { get; set; }

        public Transmission Transmission { get; set; }

        public School Owner { get; set; }

        public string ImageUrl { get; set; }

        public bool InUse { get; set; } = true;

        public string VIN { get; set; }

        public IFormFile CarImage { get; set; }

        public string Name => this.Owner.TradeMark + this.VIN;
    }
}
