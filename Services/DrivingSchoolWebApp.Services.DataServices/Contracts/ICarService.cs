namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using Models.Car;

    public interface ICarService
    {
        IEnumerable<Car> GetAllCars();

        TViewModel GetCarById<TViewModel>(int id);

        IEnumerable<TViewModel> GetCarsByOwnerTradeMark<TViewModel>(string trademark);

        IEnumerable<TViewModel> GetCarsBySchoolId<TViewModel>(int schoolId);
        
        Task<Car> Create(CreateCarInputModel model);

        Task<Car> Edit(EditCarInputModel model);

        Task Delete(CarDetailsViewModel model);

    }
}
