namespace DrivingSchoolWebApp.Data.Models
{
    using System.Collections.Generic;
    using Common;
    using Enums;

    public class Car : BaseModel<int>
    {
        public string Model { get; set; }

        public string Make { get; set; }

        public Transmission Transmission { get; set; }

        public int OwnerId { get; set; }

        public virtual School Owner { get; set; }

        public string ImageUrl { get; set; }

        public bool InUse { get; set; } = true;

        public string VIN { get; set; }

        public IEnumerable<Course> CoursesInvolved { get; set; } = new HashSet<Course>();

    }
}