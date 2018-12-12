namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.School;

    public interface ISchoolService
    {
        int Create(AppUser manager);

        void Edit(CreateSchoolInputModel model);

        void Delete(int id);

        IEnumerable<School> AllActiveSchools();

        TViewModel GetSchoolById<TViewModel>(int id);

    }
}
