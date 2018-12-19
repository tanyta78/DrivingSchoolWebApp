namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;

    public interface ITrainerService
    {
        Trainer Hire(string userId, int schoolId);

        void Edit();

        void Delete(int id);

        IEnumerable<TViewModel> TrainersBySchool<TViewModel>(int schoolId);

        TViewModel GetTrainerById<TViewModel>(int id);
      
        IEnumerable<TViewModel> AvailableTrainersBySchoolNotParticipateInCourse<TViewModel>(int schoolId);

    }
}
