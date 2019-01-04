namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.School;

    public interface ISchoolService
    {
       // void ApproveSchool(AppUser manager); 

        School Create(CreateSchoolInputModel model); 
        
        School Edit(EditSchoolInputModel model);

        School ChangeManager(int schoolId, string newManagerId);

        School Delete(int id);

        IEnumerable<TViewModel> AllActiveSchools<TViewModel>();

        TViewModel GetSchoolById<TViewModel>(int id);

        TViewModel GetSchoolByManagerName<TViewModel>(string username);

     }
}
