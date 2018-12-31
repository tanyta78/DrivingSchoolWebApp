namespace DrivingSchoolWebApp.Services.Models.School
{
    using System.Collections.Generic;
    using Data.Models;
    using Mapping;

    public class SchoolViewModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public AppUser Manager { get; set; }

        public string TradeMark { get; set; }

        public string OfficeAddress { get; set; }

        public string Phone { get; set; }
        
        public bool IsActive { get; set; }

        public int CoursesOfferedCount { get; set; }

        public int TrainersCount { get; set; } 

        public int OwnedCarsCount { get; set; } 
    }
}