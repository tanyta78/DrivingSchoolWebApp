namespace DrivingSchoolWebApp.Services.Models.Trainer
{
    using Data.Models;
    using Mapping;

    public class AvailableTrainerViewModel : IMapFrom<Trainer>
    {
        public int Id { get; set; }

        public string UserNickname { get; set; }

        public string UserAddress { get; set; }

        public string UserPhoneNumber { get; set; }

    }
}
