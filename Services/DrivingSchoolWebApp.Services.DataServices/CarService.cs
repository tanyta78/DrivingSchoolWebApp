namespace DrivingSchoolWebApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Contracts;
    using Data.Common;
    using DrivingSchoolWebApp.Data.Models;
    using Models.Car;
    using Mapping;

    public class CarService : ICarService
    {
        private readonly IRepository<Car> carRepository;
        private readonly IMapper mapper;

        public CarService(IRepository<Car> carRepository, IMapper mapper)
        {
            this.carRepository = carRepository;
            this.mapper = mapper;
        }

        public IEnumerable<Car> All()
        {
            return this.carRepository.All().ToList();
        }

        public Car Create(CreateCarInputModel model)
        {
            var car = this.mapper.Map<Car>(model);

            this.carRepository.AddAsync(car).GetAwaiter().GetResult();
            this.carRepository.SaveChangesAsync().GetAwaiter().GetResult();

            return car;
        }

        public void Delete(CreateCarInputModel model)
        {
            throw new System.NotImplementedException();
        }

       public void Edit(CreateCarInputModel model)
        {
            //todo change schoolId, inUse, Image
        }

        public TViewModel GetCarById<TViewModel>(int id)
        {
            var car = this.carRepository.All().Where(x => x.Id == id)
                           .To<TViewModel>().FirstOrDefault();
            return car;
        }
    }
}
