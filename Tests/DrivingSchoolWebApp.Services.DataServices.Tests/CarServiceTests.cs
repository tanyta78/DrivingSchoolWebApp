namespace DrivingSchoolWebApp.Services.DataServices.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Common;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;
    using Models.Account;
    using Models.Car;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class CarServiceTests
    {
        private Mock<IRepository<Car>> repository;
        private CarService carService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<Car>>();
            this.carService = new CarService(this.repository.Object);
            this.SetMapper();
        }

        [Test]
        public void GetAllCars_NoCarsInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Car>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.carService.GetAllCars();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetAllCars_CarsInDb_ReturnsAListWithCars()
        {
            var returnValue = new List<Car>()
            {
                new Car(),
                new Car(),
                new Car()
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.carService.GetAllCars().Count();

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void GetCarById_CarsInDb_ReturnsCarWithId()
        {
            var returnValue = new List<Car>()
            {
                new Car()
                {
                    Id = 1,
                    CarModel = "testModel",
                    ImageUrl = "testUrl",
                    Make = "testMake",
                    Owner = new School(),
                    OwnerId = 1,
                    Transmission = Transmission.Authomatic,
                    VIN = "df33434",
                    InUse = true
                },
                new Car(){Id = 2},
                new Car(){Id = 3}
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.carService.GetCarById<CarDetailsViewModel>(1);

            Assert.That(result.Id, Is.EqualTo(1));
        }
        
        [Test]
        public void GetCarById_NoCarWihIdInDb_ThrowsException()
        {
            var returnValue = new List<Car>()
            {
                new Car()
                {
                    Id = 1,
                    CarModel = "testModel",
                    ImageUrl = "testUrl",
                    Make = "testMake",
                    Owner = new School(),
                    OwnerId = 1,
                    Transmission = Transmission.Authomatic,
                    VIN = "df33434",
                    InUse = true
                },
                new Car(){Id = 2},
                new Car(){Id = 3}
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(()=>this.carService.GetCarById<CarDetailsViewModel>(4), Throws.ArgumentException);
        }

        [Test]
        public void Create_WithValidCarObject_ReturnsCar()
        {
            var car = new Car()
            {
                Id = 1,
                CarModel = "testModel",
                ImageUrl = "testUrl",
                Make = "testMake",
                Owner = new School(),
                OwnerId = 1,
                Transmission = Transmission.Authomatic,
                VIN = "df33434",
                InUse = true
            };

           
            this.repository.Setup(r => r.AddAsync(car)).Returns(Task.FromResult(car));

            var model = new CreateCarInputModel()
            {
                CarModel = "testModel",
                ImageUrl = "testUrl",
                Make = "testMake",
                OwnerId = 1,
                Transmission = Transmission.Authomatic,
                VIN = "df33434",
                InUse = true
            };
            var result = this.carService.Create(model).GetAwaiter().GetResult();

            Assert.That(result.CarModel,Is.EqualTo("testModel"));
        }





        private void SetMapper()
        {
            try
            {
                //Mapper.Reset();
                //Mapper.Initialize(x => { x.AddProfile<AppProfile>(); });

                AutoMapperConfig.RegisterMappings(
                    typeof(AdminPanelUsersViewModel).Assembly,
                    typeof(AppUser).Assembly,
                    typeof(CarDetailsViewModel).Assembly
                );
            }
            catch (InvalidOperationException)
            {
                //Do nothing -> AutoMapper is already initialized
            }
        }
    }
}