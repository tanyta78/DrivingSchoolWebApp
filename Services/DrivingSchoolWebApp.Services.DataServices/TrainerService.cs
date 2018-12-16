﻿namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Common;
    using Data.Models;
    using Mapping;

    public class TrainerService : ITrainerService
    {
        private readonly IRepository<Trainer> trainerRepository;
        
        public TrainerService(IRepository<Trainer> trainerRepository)
        {
            this.trainerRepository = trainerRepository;
        }

        public Trainer Hire(string userId, int schoolId)
        {
            var trainer = new Trainer()
            {
                UserId = userId,
                SchoolId = schoolId
            };

            this.trainerRepository.AddAsync(trainer).GetAwaiter().GetResult();
            this.trainerRepository.SaveChangesAsync().GetAwaiter().GetResult();

            return trainer;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Edit()
        {
            throw new System.NotImplementedException();

        }

        public TViewModel GetTrainerById<TViewModel>(int id)
        {
            var trainer = this.trainerRepository.All()
                               .Where(x => x.Id == id)
                               .To<TViewModel>()
                               .FirstOrDefault();
            return trainer;
        }

        public IEnumerable<TViewModel> TrainersBySchool<TViewModel>(int schoolId)
        {
            var trainers = this.trainerRepository.All()
                               .Where(x => x.SchoolId == schoolId)
                               .To<TViewModel>()
                               .ToList();
            return trainers;
        }
    }
}