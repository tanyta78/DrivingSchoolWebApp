namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.School;

    public interface ISchoolService
    {
        School ApproveSchool(AppUser manager); 

        School Create(CreateSchoolInputModel model); 
        
        School Edit(EditSchoolInputModel model);

        School ChangeManager(int schoolId, AppUser newManager);

        void Delete(int id);

        IEnumerable<TViewModel> AllActiveSchools<TViewModel>();

        School GetSchoolById(int id);

        School GetSchoolByManagerName(string username);


    }
}
