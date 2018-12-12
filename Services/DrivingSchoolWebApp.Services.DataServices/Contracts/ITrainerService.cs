namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;

    public interface ITrainerService
    {
        int Hire(string userId, int schoolId);

        void Edit();

        void Delete(int id);

        IEnumerable<TViewModel> TrainersBySchool<TViewModel>(int schoolId);

        TViewModel GetTrainerById<TViewModel>(int id);
    }
}
