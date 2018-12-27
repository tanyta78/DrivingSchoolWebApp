namespace DrivingSchoolWebApp.Services.Models.Trainer
{
    using Data.Models;
    using Mapping;

    public class DeleteTrainerViewModel : IMapFrom<Trainer>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserNickname { get; set; }

        public int SchoolId { get; set; }

        public string SchoolTradeMark { get; set; }

        public bool IsAvailable { get; set; }
    }
}
