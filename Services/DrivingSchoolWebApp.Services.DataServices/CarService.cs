﻿namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using DrivingSchoolWebApp.Data.Models;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Models.Car;

    public class CarService : BaseService, ICarService
    {
        private readonly IRepository<Car> carRepository;


        public CarService(IRepository<Car> carRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper) : base(userManager, signInManager, mapper)
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
            var imageUrl = await Helpers.UploadImage(account, model.CarImage, model.Name);

            var car = this.Mapper.Map<Car>(model);
            car.ImageUrl = imageUrl;

            await this.carRepository.AddAsync(car);
            await this.carRepository.SaveChangesAsync();

            return car;
        }

        public async Task Delete(int id)
        {
            var car = this.GetCarById<Car>(id);
            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);

            if (!this.HasRightsToEditOrDelete(id, username))
            {
                //todo throw custom error message
                throw new OperationCanceledException("You do not have rights for this operation!");
            }

            this.carRepository.Delete(car);
            await this.carRepository.SaveChangesAsync();
        }

        public async Task<Car> Edit(EditCarInputModel model)
        {
            //todo check model validation in controller?!?
            //todo change inUse, Image, VIN
            var car = this.GetCarById<Car>(model.Id);
            var username = this.UserManager.GetUserName(ClaimsPrincipal.Current);
            
            if (!this.HasRightsToEditOrDelete(model.Id, username))
            {
               throw new OperationCanceledException("You do not have rights for this operation!");
            }

            car.InUse = model.InUse;
            car.VIN = model.VIN;

            var name = car.Owner.TradeMark + model.VIN;
            var account = Helpers.SetCloudinary();
            var imageUrl = await Helpers.UploadImage(account, model.CarImage, name);

            this.carRepository.Update(car);

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

        private bool HasRightsToEditOrDelete(int carId, string username)
        {
            var car = this.GetCarById<Car>(carId);
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            //todo check user and car for null; to add include if needed

            var roles = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var hasRights = roles.Any(x => x == "Admin");
            var isOwner = username == car.Owner.Manager.UserName;

            return isOwner || hasRights;
        }
    }
}