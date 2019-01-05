namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using DrivingSchoolWebApp.Data.Models;
    using Mapping;
    using Models.Car;

    public class CarService : ICarService
    {
        private readonly IRepository<Car> carRepository;


        public CarService(IRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public IEnumerable<Car> GetAllCars()
        {
            var result = this.carRepository.All().ToList();
            return result;
        }

        public async Task<Car> Create(CreateCarInputModel model)
        {
            var account = Helpers.SetCloudinary();
            var imageUrl = await Helpers.UploadImage(account, model.CarImage, model.VIN);

           var car = Mapper.Map<Car>(model);
            car.ImageUrl = imageUrl;
            await this.carRepository.AddAsync(car);
            await this.carRepository.SaveChangesAsync();

            return car;
        }

        public async Task<Car> Delete(CarDetailsViewModel model)
        {
            var car = this.GetCarById(model.Id);

            this.carRepository.Delete(car);
            await this.carRepository.SaveChangesAsync();

            return car;
        }

        public async Task<Car> Edit(EditCarInputModel model)
        {
            //todo check model validation in controller?!?
            //todo change inUse, Image, VIN
            var car = this.GetCarById(model.Id);

            car.InUse = model.InUse;
            car.VIN = model.VIN;

            var name = car.Owner.TradeMark + model.VIN;
            var account = Helpers.SetCloudinary();
            if (model.CarImage != null)
            {
                var imageUrl = await Helpers.UploadImage(account, model.CarImage, name);
                car.ImageUrl = imageUrl;

            }

            this.carRepository.Update(car);
            await this.carRepository.SaveChangesAsync();

            return car;
        }

        public TViewModel GetCarById<TViewModel>(int id)
        {
            var car = this.carRepository.All().Where(x => x.Id == id)
                .To<TViewModel>().FirstOrDefault();

            if (car == null)
            {
                throw new ArgumentException("No car with id in db");
            }

            return car;
        }

        public IEnumerable<TViewModel> GetCarsByOwnerTradeMark<TViewModel>(string trademark)
        {
            var cars = this.carRepository.All().Where(x => x.Owner.TradeMark == trademark).ProjectTo<TViewModel>().ToList();

            return cars;
        }

        public IEnumerable<TViewModel> GetCarsBySchoolId<TViewModel>(int schoolId)
        {
            var cars = this.carRepository.All().Where(x => x.OwnerId == schoolId).ProjectTo<TViewModel>().ToList();

            return cars;
        }


        private Car GetCarById(int id)
        {
            var car = this.carRepository.All()
               .FirstOrDefault(x => x.Id == id);

            if (car == null)
            {
                throw new ArgumentException("No car with id in db");
            }

            return car;
        }
    }
}
