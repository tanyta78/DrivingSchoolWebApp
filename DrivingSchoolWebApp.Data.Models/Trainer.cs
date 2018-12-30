namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Common;

    public class Trainer : BaseModel<int>
    {
        public Trainer()
        {
           this.IsAvailable = true;
        }

        public string UserId { get; set; }

        public virtual AppUser User { get; set; }

        public int SchoolId { get; set; }

        public virtual School School { get; set; }

        public DateTime HireDate { get; set; } 

        public bool IsAvailable { get; set; }

        public DateTime AvailableLessonDay => this.HireDate.Date.AddDays(7);

        public DateTime AvailableStartTime { get; set; }

        public virtual IEnumerable<Course> CoursesInvolved { get; set; } = new HashSet<Course>();
    }
}