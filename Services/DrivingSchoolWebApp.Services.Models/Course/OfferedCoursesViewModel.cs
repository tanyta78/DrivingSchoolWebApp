namespace DrivingSchoolWebApp.Services.Models.Course
{
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;

    public class OfferedCoursesViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public Category Category { get; set; }

        public string TrainerNickName { get; set; }

        public string CarMake { get; set; }

        public string CarModel { get; set; }

        public string CarTransmission { get; set; }

        public string CarVIN { get; set; }
        
        public decimal Price { get; set; }

    }
}
