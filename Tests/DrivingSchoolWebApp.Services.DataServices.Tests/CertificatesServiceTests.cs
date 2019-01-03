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
    using Models.Certificate;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class CertificatesServiceTests
    {
        private Mock<IRepository<Certificate>> repository;
        private CertificateService certificateService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<Certificate>>();
            this.certificateService = new CertificateService(this.repository.Object);
            this.SetMapper();
        }

        [Test]
        public void All_NoCertificatesInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Certificate>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.certificateService.All();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void All_CertificatesInDb_ReturnsAListWithCertificates()
        {
            var returnValue = new List<Certificate>()
            {
                new Certificate(),
                new Certificate(),
                new Certificate()
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.certificateService.All().Count();

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void AllGeneric_CertificatesInDb_ReturnsAListWithGenericType()
        {
            var courseA = new Course()
            {
                Id = 1,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 1,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "1111",
                    Transmission = Transmission.Authomatic
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1
            };
            var courseB = new Course()
            {
                Id = 2,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 2,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "2222",
                    Transmission = Transmission.Manual
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1,
            };

            var customerA = new Customer()
            {
                Id = 1,
                UserId = "adsf2",
                User = new AppUser()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN"
                }
            };

            var returnValue = new List<Certificate>()
            {
                new Certificate()
                {
                    Id = 1,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 1,
                    Customer = customerA
                },
                new Certificate()
                {
                    Id = 2,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 1,
                    Customer = customerA
                },
                new Certificate()
                {
                    Id = 3,
                    Course = courseB,
                    CourseId = 2,
                    CustomerId = 1,
                    Customer = customerA
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.certificateService.All<AllCertificatesViewModel>().ToList();

            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result,Is.TypeOf<List<AllCertificatesViewModel>>());
        }

         [Test]
        public void GetCertificateByIdGeneric_CertificatesInDb_ReturnsGenericType()
        {
            var courseA = new Course()
            {
                Id = 1,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 1,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "1111",
                    Transmission = Transmission.Authomatic
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1
            };
            var courseB = new Course()
            {
                Id = 2,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 2,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "2222",
                    Transmission = Transmission.Manual
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1,
            };

            var customerA = new Customer()
            {
                Id = 1,
                UserId = "asdf1",
                User = new AppUser()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN"
                }
            };

            var customerB = new Customer()
            {
                Id = 2,
                UserId = "asdf2",
                User = new AppUser()
                {
                    FirstName = "TestFN2",
                    LastName = "TestLN2"
                }
            };

            var returnValue = new List<Certificate>()
            {
                new Certificate()
                {
                    Id = 1,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 2,
                    Customer = customerB
                },
                new Certificate()
                {
                    Id = 2,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 1,
                    Customer = customerA
                },
                new Certificate()
                {
                    Id = 3,
                    Course = courseB,
                    CourseId = 2,
                    CustomerId = 1,
                    Customer = customerA
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.certificateService.GetCertificateById<AllCertificatesViewModel>(2);

            Assert.That(result.CourseId, Is.EqualTo(1));
            Assert.That(result.CustomerId, Is.EqualTo(1));

            Assert.That(result,Is.TypeOf<AllCertificatesViewModel>());
        }

          [Test]
        public void GetCertificateByIdGeneric_UnValidIdCertificatesInDb_ThrowsException()
        {
            var courseA = new Course()
            {
                Id = 1,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 1,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "1111",
                    Transmission = Transmission.Authomatic
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1
            };
            var courseB = new Course()
            {
                Id = 2,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 2,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "2222",
                    Transmission = Transmission.Manual
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1,
            };

            var customerA = new Customer()
            {
                Id = 1,
                UserId = "asdf1",
                User = new AppUser()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN"
                }
            };

            var customerB = new Customer()
            {
                Id = 2,
                UserId = "asdf2",
                User = new AppUser()
                {
                    FirstName = "TestFN2",
                    LastName = "TestLN2"
                }
            };

            var returnValue = new List<Certificate>()
            {
                new Certificate()
                {
                    Id = 1,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 2,
                    Customer = customerB
                },
                new Certificate()
                {
                    Id = 2,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 1,
                    Customer = customerA
                },
                new Certificate()
                {
                    Id = 3,
                    Course = courseB,
                    CourseId = 2,
                    CustomerId = 1,
                    Customer = customerA
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

           Assert.That(() => this.certificateService.GetCertificateById<AllCertificatesViewModel>(4), Throws.ArgumentException);
        }

         [Test]
        public void GetCertificateByCustomerIdGeneric_UnValidCustomerIdCertificatesInDb_ReturnsEmptyList()
        {
            var courseA = new Course()
            {
                Id = 1,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 1,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "1111",
                    Transmission = Transmission.Authomatic
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1
            };
            var courseB = new Course()
            {
                Id = 2,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 2,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "2222",
                    Transmission = Transmission.Manual
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1,
            };

            var customerA = new Customer()
            {
                Id = 1,
                UserId = "asdf1",
                User = new AppUser()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN"
                }
            };

            var customerB = new Customer()
            {
                Id = 2,
                UserId = "asdf2",
                User = new AppUser()
                {
                    FirstName = "TestFN2",
                    LastName = "TestLN2"
                }
            };

            var returnValue = new List<Certificate>()
            {
                new Certificate()
                {
                    Id = 1,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 2,
                    Customer = customerB
                },
                new Certificate()
                {
                    Id = 2,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 1,
                    Customer = customerA
                },
                new Certificate()
                {
                    Id = 3,
                    Course = courseB,
                    CourseId = 2,
                    CustomerId = 1,
                    Customer = customerA
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());
            var result = this.certificateService.GetCertificatesByCustomerId<AllCertificatesViewModel>(4).ToList();

           Assert.That(result.Count(),Is.EqualTo(0));
           Assert.That(result,Is.Empty);
           Assert.That(result,Is.TypeOf<List<AllCertificatesViewModel>>());

        }

         [Test]
        public void GetCertificateByCourseIdGeneric_ValidCourseIdCertificatesInDb_ReturnsGenericWithWantedCourseId()
        {
            var courseA = new Course()
            {
                Id = 1,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 1,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "1111",
                    Transmission = Transmission.Authomatic
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1
            };
            var courseB = new Course()
            {
                Id = 2,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 2,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "2222",
                    Transmission = Transmission.Manual
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1,
            };

            var customerA = new Customer()
            {
                Id = 1,
                UserId = "asdf1",
                User = new AppUser()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN"
                }
            };

            var customerB = new Customer()
            {
                Id = 2,
                UserId = "asdf2",
                User = new AppUser()
                {
                    FirstName = "TestFN2",
                    LastName = "TestLN2"
                }
            };

            var returnValue = new List<Certificate>()
            {
                new Certificate()
                {
                    Id = 1,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 2,
                    Customer = customerB
                },
                new Certificate()
                {
                    Id = 2,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 1,
                    Customer = customerA
                },
                new Certificate()
                {
                    Id = 3,
                    Course = courseB,
                    CourseId = 2,
                    CustomerId = 1,
                    Customer = customerA
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());
            var result = this.certificateService.GetCertificatesByCourseId<AllCertificatesViewModel>(2).ToList();

           Assert.That(result.Count(),Is.EqualTo(1));
           Assert.That(result,Is.TypeOf<List<AllCertificatesViewModel>>());

        }

         [Test]
        public void GetCertificateBySchoolIdGeneric_ValidSchoolIdCertificatesInDb_ReturnsGenericWithWantedSchoolId()
        {
            var courseA = new Course()
            {
                Id = 1,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 1,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "1111",
                    Transmission = Transmission.Authomatic
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1
            };
            var courseB = new Course()
            {
                Id = 2,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty"
                    }
                },
                TrainerId = 1,
                CarId = 2,
                Car = new Car()
                {
                    CarModel = "FCModel",
                    Make = "2222",
                    Transmission = Transmission.Manual
                },
                School = new School()
                {
                    TradeMark = "BestSchool"
                },
                SchoolId = 1,
            };

            var customerA = new Customer()
            {
                Id = 1,
                UserId = "asdf1",
                User = new AppUser()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN"
                }
            };

            var customerB = new Customer()
            {
                Id = 2,
                UserId = "asdf2",
                User = new AppUser()
                {
                    FirstName = "TestFN2",
                    LastName = "TestLN2"
                }
            };

            var returnValue = new List<Certificate>()
            {
                new Certificate()
                {
                    Id = 1,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 2,
                    Customer = customerB
                },
                new Certificate()
                {
                    Id = 2,
                    Course = courseA,
                    CourseId = 1,
                    CustomerId = 1,
                    Customer = customerA
                },
                new Certificate()
                {
                    Id = 3,
                    Course = courseB,
                    CourseId = 2,
                    CustomerId = 1,
                    Customer = customerA
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());
            var result = this.certificateService.GetCertificatesBySchoolId<AllCertificatesViewModel>(1).ToList();

           Assert.That(result.Count(),Is.EqualTo(3));
           Assert.That(result,Is.TypeOf<List<AllCertificatesViewModel>>());

        }

         [Test]
        public void Create_ValidInputModel_ReturnsCreatedCertificate()
        {
            var certificate = new Certificate()
            {
                CourseId = 1,
                CustomerId = 2
            };

            this.repository.Setup(r => r.AddAsync(certificate)).Returns(Task.FromResult(certificate));

            var model = new CreateCertificateInputModel()
            {
                CourseId = 1,
                CustomerId = 2
            };

            var result = this.certificateService.Create(model).GetAwaiter().GetResult();

            Assert.That(result.CourseId, Is.EqualTo(1));
            Assert.That(result.CustomerId, Is.EqualTo(2));

        }

        private void SetMapper()
        {
            try
            {
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
