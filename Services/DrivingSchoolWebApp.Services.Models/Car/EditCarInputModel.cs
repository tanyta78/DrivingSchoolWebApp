namespace DrivingSchoolWebApp.Services.Models.Car
{
    using Data.Models;
    using Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditCarInputModel : IMapFrom<Car>
    {
        public int Id { get; set; }
        
        public string ImageUrl { get; set; }

        public bool InUse { get; set; } 

        public string VIN { get; set; }

        public IFormFile CarImage { get; set; }
    }
}
