namespace DrivingSchoolWebApp.Services.Models.Trainer
{
    using System;
    using Data.Models;
    using Mapping;

    public class TrainerViewModel : IMapFrom<Trainer>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserNickname { get; set; }

        public string UserAddress { get; set; }

        public string UserPhoneNumber { get; set; }

        public int SchoolId { get; set; }

        public string SchoolTradeMark { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime AvailableLessonDay => this.HireDate.Date.AddDays(7);

        public DateTime AvailableStartTime { get; set; }

        public int CoursesInvolvedCount { get; set; }

    }
}
