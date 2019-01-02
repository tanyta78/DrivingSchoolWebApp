namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public IEnumerable<Car> All()
        {
            return this.carRepository.All().ToList();
        }

        public async Task<Car> Create(CreateCarInputModel model)
        {
            var account = Helpers.SetCloudinary();
            var imageUrl = await Helpers.UploadImage(account, model.CarImage, model.VIN);

            var car = new Car
            {
                Make = model.Make,
                CarModel = model.CarModel,
                OwnerId = model.OwnerId,
                VIN = model.VIN,
                Transmission = model.Transmission,
                ImageUrl = imageUrl
            };


            await this.carRepository.AddAsync(car);
            await this.carRepository.SaveChangesAsync();

            return car;
        }

        public async Task Delete(CarDetailsViewModel model)
        {
            var car = this.GetCarById(model.Id);
            //var username = model.Username;

            //if (!this.HasRightsToEditOrDelete(model.Id, username))
            //{
            //    //todo throw custom error message
            //    throw new OperationCanceledException("You do not have rights for this operation!");
            //}

            this.carRepository.Delete(car);
            await this.carRepository.SaveChangesAsync();
        }

        public async Task<Car> Edit(EditCarInputModel model)
        {
            //todo check model validation in controller?!?
            //todo change inUse, Image, VIN
            var car = this.GetCarById(model.Id);
            //var username = model.Username;

            //if (!this.HasRightsToEditOrDelete(model.Id, username))
            //{
            //    throw new OperationCanceledException("You do not have rights for this operation!");
            //}

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

        //private bool HasRightsToEditOrDelete(int carId, string username)
        //{
        //    var car = this.GetCarById(carId);
        //    var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

        //    //todo check user and car for null; to add include if needed

        //    var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

        //    var hasRights = roles.Any(x => x == "Admin");
        //    var isOwner = username == car.Owner.Manager.UserName;

        //    return isOwner || hasRights;
        //}

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
