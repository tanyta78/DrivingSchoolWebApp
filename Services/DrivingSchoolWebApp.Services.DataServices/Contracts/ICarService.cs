namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Models.Car;

    public interface ICarService
    {
        IEnumerable<Car> All();

        TViewModel GetCarById<TViewModel>(int id);

        int Create(CreateCarInputModel model);

        void Edit(CreateCarInputModel model);

        void Delete(CreateCarInputModel model);

    }
}
