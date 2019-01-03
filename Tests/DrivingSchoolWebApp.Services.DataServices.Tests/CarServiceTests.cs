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
    using Microsoft.AspNetCore.Http;
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

            Assert.That(() => this.carService.GetCarById<CarDetailsViewModel>(4), Throws.ArgumentException);
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

            Assert.That(result.CarModel, Is.EqualTo("testModel"));
        }

        [Test]
        public void Edit_WithValidCarObject_ReturnsCar()
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
                VIN = "77777",
                InUse = true
            };
            var returnValue = new List<Car>()
            {
                car
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            this.repository.Setup(r => r.Update(car)).Callback(() => new Car()
            {
                Id = 1,
                CarModel = "testModel",
                ImageUrl = "testUrl",
                Make = "testMake",
                Owner = new School(),
                OwnerId = 1,
                Transmission = Transmission.Authomatic,
                VIN = "AA1111AA",
                InUse = true
            });

            var model = new EditCarInputModel()
            {
                Id = 1,
                ImageUrl = "testUrlNew",
                VIN = "AA1111AA",
                InUse = false
            };
            var result = this.carService.Edit(model).GetAwaiter().GetResult();

            Assert.That(result.VIN, Is.EqualTo("AA1111AA"));
            Assert.That(result.ImageUrl, Is.EqualTo("testUrl"));
            Assert.That(result.InUse, Is.EqualTo(false));
            Assert.That(result.Id,Is.EqualTo(1));

            Assert.That(result.CarModel, Is.EqualTo("testModel"));
            Assert.That(result,Is.TypeOf<Car>());
        }

        [Test]
        public void Edit_WithEmptyImageFile_ThrowsException()
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
                VIN = "77777",
                InUse = true
            };
            var returnValue = new List<Car>()
            {
                car
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            this.repository.Setup(r => r.Update(car)).Callback(() => new Car()
            {
                Id = 1,
                CarModel = "testModel",
                ImageUrl = "testUrl",
                Make = "testMake",
                Owner = new School(),
                OwnerId = 1,
                Transmission = Transmission.Authomatic,
                VIN = "AA1111AA",
                InUse = true
            });

            var fileformMock = new Mock<IFormFile>();
            var model = new EditCarInputModel()
            {
                Id = 1,
                ImageUrl = "testUrlNew",
                VIN = "AA1111AA",
                InUse = false,
                CarImage = fileformMock.Object
            };

            Assert.That(() => this.carService.Edit(model).GetAwaiter().GetResult(), Throws.Exception);
        }

        [Test]
        public void Edit_CarObjectDoNotExistInDb_ThrowsException()
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
                VIN = "77777",
                InUse = true
            };
            var returnValue = new List<Car>()
            {
                car
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            this.repository.Setup(r => r.Update(car)).Callback(() => new Car()
            {
                Id = 1,
                CarModel = "testModel",
                ImageUrl = "testUrl",
                Make = "testMake",
                Owner = new School(),
                OwnerId = 1,
                Transmission = Transmission.Authomatic,
                VIN = "AA1111AA",
                InUse = true
            });

            var model = new EditCarInputModel()
            {
                Id = 2,
                ImageUrl = "testUrl",
                VIN = "AA1111AA",
                InUse = true
            };

            Assert.That(() => this.carService.Edit(model).GetAwaiter().GetResult(), Throws.ArgumentException);
        }

        [Test]
        public void GetCarByOwnerTradeMark_WithValidData_ReturnsCarsWithWantedTradeMark()
        {
            var returnValue = new List<Car>()
            {
                new Car()
                {
                    Id = 1,
                    CarModel = "testModel",
                    ImageUrl = "testUrl",
                    Make = "testMake",
                    Owner = new School()
                    {
                        TradeMark = "TheBestOne"
                    },
                    OwnerId = 1,
                    Transmission = Transmission.Authomatic,
                    VIN = "AA3311",
                    InUse = true
                },
                new Car()
                {
                    Id = 2,
                    CarModel = "testModel2",
                    ImageUrl = "testUrl2",
                    Make = "testMake2",
                    Owner = new School()
                    {
                        TradeMark = "TheBestOne2"
                    },
                    OwnerId = 2,
                    Transmission = Transmission.Authomatic,
                    VIN = "AA3322",
                    InUse = true
                },
                new Car()
                {
                    Id = 3,
                    CarModel = "testModel3",
                    ImageUrl = "testUrl3",
                    Make = "testMake3",
                    Owner = new School()
                    {
                        TradeMark = "TheBestOne"
                    },
                    OwnerId = 1,
                    Transmission = Transmission.Authomatic,
                    VIN = "AA3333",
                    InUse = true
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.carService.GetCarsByOwnerTradeMark<CarDetailsViewModel>("TheBestOne");

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetCarBySchoolId_WithValidData_ReturnsCarsWithWantedSchoolId()
        {
            var returnValue = new List<Car>()
            {
                new Car()
                {
                    Id = 1,
                    CarModel = "testModel",
                    ImageUrl = "testUrl",
                    Make = "testMake",
                    Owner = new School()
                    {
                        TradeMark = "TheBestOne"
                    },
                    OwnerId = 1,
                    Transmission = Transmission.Authomatic,
                    VIN = "AA3311",
                    InUse = true
                },
                new Car()
                {
                    Id = 2,
                    CarModel = "testModel2",
                    ImageUrl = "testUrl2",
                    Make = "testMake2",
                    Owner = new School()
                    {
                        TradeMark = "TheBestOne2"
                    },
                    OwnerId = 2,
                    Transmission = Transmission.Authomatic,
                    VIN = "AA3322",
                    InUse = true
                },
                new Car()
                {
                    Id = 3,
                    CarModel = "testModel3",
                    ImageUrl = "testUrl3",
                    Make = "testMake3",
                    Owner = new School()
                    {
                        TradeMark = "TheBestOne2"
                    },
                    OwnerId = 2,
                    Transmission = Transmission.Authomatic,
                    VIN = "AA3333",
                    InUse = true
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.carService.GetCarsBySchoolId<CarDetailsViewModel>(2);

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Delete_WithValidCarObject_ReturnsCar()
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

            var returnValue=new List<Car>()
            {
                car
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);
            this.repository.Setup(r => r.Delete(car)).Callback(() => new Car()
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
            });

           var model = new CarDetailsViewModel()
           {
               Id = 1
           };

            var result = this.carService.Delete(model).GetAwaiter().GetResult();

            Assert.That(result.CarModel, Is.EqualTo("testModel"));
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