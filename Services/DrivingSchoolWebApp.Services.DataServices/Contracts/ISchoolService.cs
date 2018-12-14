namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.School;

    public interface ISchoolService
    {
        School Create(AppUser manager); 

        void Edit(CreateSchoolInputModel model);

        void Delete(int id);

        IEnumerable<TViewModel> AllActiveSchools<TViewModel>();

        TViewModel GetSchoolById<TViewModel>(int id);

        TViewModel GetSchoolByManagerName<TViewModel>(string username);


    }
}
