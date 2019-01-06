namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.Trainer;

    public interface ITrainerService
    {
        Trainer Hire(CreateTrainerInputModel model);

        //void Edit();

        //void Delete(int id);

        IEnumerable<TViewModel> TrainersBySchoolId<TViewModel>(int schoolId);

        TViewModel GetTrainerById<TViewModel>(int id);
      
        IEnumerable<TViewModel> AvailableTrainersBySchoolIdNotParticipateInCourse<TViewModel>(int schoolId);

        Trainer GetTrainerByUserId(string userId);

    }
}
