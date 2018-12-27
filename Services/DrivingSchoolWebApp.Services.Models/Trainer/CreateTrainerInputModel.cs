namespace DrivingSchoolWebApp.Services.Models.Trainer
{
    using System;
    using Data.Models;
    using Mapping;

    public class CreateTrainerInputModel : IMapFrom<Trainer>
    {
        public string UserId { get; set; }

        public int SchoolId { get; set; }

        public DateTime HireDate { get; set; }


    }
}
