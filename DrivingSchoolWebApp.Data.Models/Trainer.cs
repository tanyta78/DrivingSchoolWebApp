namespace DrivingSchoolWebApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Common;

    public class Trainer : BaseModel<int>
    {
        public AppUser User { get; set; }

        public virtual School School { get; set; }

        public DateTime AvailableLessonDay { get; set; }

        public DateTime AvailableStartTime { get; set; }

        public IEnumerable<Course> CoursesInvolved { get; set; }
    }
}