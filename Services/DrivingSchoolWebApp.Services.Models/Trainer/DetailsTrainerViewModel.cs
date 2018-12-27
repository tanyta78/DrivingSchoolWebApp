namespace DrivingSchoolWebApp.Services.Models.Trainer
{
    using System;
    using Data.Models;
    using Mapping;

    public class DetailsTrainerViewModel : IMapFrom<Trainer>
    {
        public int Id { get; set; }

        public string UserNickname { get; set; }

        public string UserAddress { get; set; }

        public string UserPhoneNumber { get; set; }

        public string SchoolTradeMark { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime AvailableLessonDay => this.HireDate.Date.AddDays(7);

    }
}
