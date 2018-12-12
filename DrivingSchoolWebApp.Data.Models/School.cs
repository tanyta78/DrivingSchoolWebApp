namespace DrivingSchoolWebApp.Data.Models
{
    using System.Collections.Generic;
    using Common;

    public class School : BaseModel<int>
    {
        public School()
        {
            this.IsActive = true;
        }

        public string ManagerId { get; set; }

        public virtual AppUser Manager { get; set; }

        public string TradeMark { get; set; }

        public string OfficeAddress { get; set; }

        public bool IsActive { get; set; }

        public virtual IEnumerable<Course> CoursesOffered { get; set; } = new HashSet<Course>();

        public virtual IEnumerable<Trainer> Trainers { get; set; } = new HashSet<Trainer>();

        public virtual IEnumerable<Order> Orders { get; set; } = new HashSet<Order>();

        public virtual IEnumerable<Car> OwnedCars { get; set; } = new HashSet<Car>();

    }
}
