namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.School;

    public interface ISchoolService
    {
        School Create(AppUser manager); 

        School Edit(EditSchoolInputModel model);

        School ChangeManager(int schoolId, AppUser newManager);

        void Delete(int id);

        IEnumerable<TViewModel> AllActiveSchools<TViewModel>();

        TViewModel GetSchoolById<TViewModel>(int id);

        TViewModel GetSchoolByManagerName<TViewModel>(string username);


    }
}
