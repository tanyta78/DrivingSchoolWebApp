namespace DrivingSchoolWebApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Mapping;
    using Models.Trainer;

    public class TrainerService : ITrainerService
    {
        private readonly IRepository<Trainer> trainerRepository;
        
        public TrainerService(IRepository<Trainer> trainerRepository)
        {
            this.trainerRepository = trainerRepository;
        }

        public Trainer Hire(CreateTrainerInputModel model)
        {
            var trainer = Mapper.Map<Trainer>(model);
            
            this.trainerRepository.AddAsync(trainer).GetAwaiter().GetResult();
            this.trainerRepository.SaveChangesAsync().GetAwaiter().GetResult();

            return trainer;
        }

       public TViewModel GetTrainerById<TViewModel>(int id)
        {
            var trainer = this.trainerRepository.All()
                               .Where(x => x.Id == id)
                               .To<TViewModel>()
                               .FirstOrDefault();
            return trainer;
        }

        public IEnumerable<TViewModel> AvailableTrainersBySchoolIdNotParticipateInCourse<TViewModel>(int schoolId)
        {
            var trainers = this.trainerRepository.All()
                               .Where(x => x.SchoolId == schoolId && !x.CoursesInvolved.Any() && x.IsAvailable)
                               .To<TViewModel>()
                               .ToList();
            return trainers;
        }

        public IEnumerable<TViewModel> TrainersBySchoolId<TViewModel>(int schoolId)
        {
            var trainers = this.trainerRepository.All()
                               .Where(x => x.SchoolId == schoolId)
                               .To<TViewModel>()
                               .ToList();
            return trainers;
        }

        public Trainer GetTrainerByUserId(string userId)
        {
            var trainer = this.trainerRepository
                .All()
                .FirstOrDefault(t => t.UserId == userId);
            return trainer;
        }
    }
}
