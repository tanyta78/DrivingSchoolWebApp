namespace DrivingSchoolWebApp.Services.Models.School
{
    using System.Collections.Generic;
    using Data.Models;
    using Mapping;

    public class CreateSchoolInputModel:IMapFrom<School>
    {
        public AppUser Manager { get; set; }

        public string TradeMark { get; set; }

        public string OfficeAddress { get; set; }

        public bool IsActive { get; set; }

        public virtual IEnumerable<Course> CoursesOffered { get; set; } = new HashSet<Course>();

        public virtual IEnumerable<Trainer> Trainers { get; set; } = new HashSet<Trainer>();

        public virtual IEnumerable<Order> Orders { get; set; } = new HashSet<Order>();

        public virtual IEnumerable<Car> OwnedCars { get; set; } = new HashSet<Car>();

    }
}