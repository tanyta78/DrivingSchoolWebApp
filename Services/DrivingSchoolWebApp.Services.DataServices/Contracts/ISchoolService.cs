namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.School;

    public interface ISchoolService
    {
        void ApproveSchool(AppUser manager); 

        int Create(CreateSchoolInputModel model); 
        
        int Edit(EditSchoolInputModel model);

        int ChangeManager(int schoolId, AppUser newManager);

        void Delete(int id);

        IEnumerable<TViewModel> AllActiveSchools<TViewModel>();

        TViewModel GetSchoolById<TViewModel>(int id);

        TViewModel GetSchoolByManagerName<TViewModel>(string username);

     }
}
