namespace DrivingSchoolWebApp.Services.DataServices.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Common;
    using Data.Models;
    using Mapping;
    using Models.Account;
    using Models.Car;
    using Models.School;
    using Models.Trainer;
    using Moq;
    using NUnit.Framework;
    using NUnit.Framework.Internal;

    [TestFixture]
    public class TrainerServiceTests
    {
        private Mock<IRepository<Trainer>> repository;
        private TrainerService trainerService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<Trainer>>();
            this.trainerService = new TrainerService(this.repository.Object);
            this.SetMapper();
        }

        [Test]
        public void Hire_ValidInputModel_ReturnsCreatedTrainer()
        {
            var trainer = new Trainer()
            {
               UserId = "asdf",
               SchoolId = 1,
               HireDate = DateTime.Now
            };

            this.repository.Setup(r => r.AddAsync(trainer)).Returns(Task.FromResult(trainer));

            var model = new CreateTrainerInputModel()
            {
                UserId = "asdf",
                SchoolId = 1,
                HireDate = DateTime.Now
            };

            var result = this.trainerService.Hire(model);

            Assert.That(result.UserId, Is.EqualTo("asdf"));
            Assert.That(result.SchoolId, Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<Trainer>());
        }

          [Test]
        public void GenericGetSTrainerById_TrainersInDb_ReturnsTrainerWithId()
        {
            var returnValue = new List<Trainer>()
            {
                new Trainer()
                {
                    Id = 1,
                    User = new AppUser()
                    {
                        Nickname = "asd",
                        Address = "adr",
                        PhoneNumber = "2232",
                    },
                    CoursesInvolved = new List<Course>()
                },
                new Trainer()
                {
                    Id = 2,
                    User = new AppUser()
                    {
                        Nickname = "asd2",
                        Address = "adr2",
                        PhoneNumber = "22322",
                    },
                    CoursesInvolved = new List<Course>()
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.trainerService.GetTrainerById<AllTrainerViewModel>(2);

            Assert.That(result.Id, Is.EqualTo(2));
            Assert.That(result, Is.TypeOf<AllTrainerViewModel>());
        }

        [Test]
        public void GenericGetTrainerById_NoTrainerWithIdInDb_ReturnsNull()
        {
            var returnValue = new List<Trainer>()
            {
                new Trainer()
                {
                    Id = 1,
                    User = new AppUser()
                    {
                        Nickname = "asd",
                        Address = "adr",
                        PhoneNumber = "2232",
                    },
                    CoursesInvolved = new List<Course>()
                },
                new Trainer()
                {
                    Id = 2,
                    User = new AppUser()
                    {
                        Nickname = "asd2",
                        Address = "adr2",
                        PhoneNumber = "22322",
                    },
                    CoursesInvolved = new List<Course>()
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.trainerService.GetTrainerById<AllSchoolViewModel>(5);

            Assert.That(result,Is.Null);
        }

        [Test]
        public void GenericGetSchoolById_NoSchoolsInDb_ReturnsNull()
        {
            var returnValue = new List<Trainer>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.trainerService.GetTrainerById<AllTrainerViewModel>(5);
            Assert.That(result,Is.Null);
        }

        [Test]
        public void GenericGetSTrainerBySchoolId_TrainersInDb_ReturnsTrainersWithSchoolId()
        {
            var returnValue = new List<Trainer>()
            {
                new Trainer()
                {
                    Id = 1,
                    User = new AppUser()
                    {
                        Nickname = "asd",
                        Address = "adr",
                        PhoneNumber = "2232",
                    },
                    SchoolId = 1,
                    CoursesInvolved = new List<Course>()
                },
                new Trainer()
                {
                    Id = 2,
                    User = new AppUser()
                    {
                        Nickname = "asd2",
                        Address = "adr2",
                        PhoneNumber = "22322",
                    },
                    SchoolId = 2,
                    CoursesInvolved = new List<Course>()
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.trainerService.TrainersBySchoolId<AllTrainerViewModel>(2).ToList();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<List<AllTrainerViewModel>>());
        }

        [Test]
        public void AvailableTrainersBySchoolIdNotParticipateInCourse_TrainersInDb_ReturnsTrainersWithSchoolIdNoParticipatyedInCourseAndAvailible()
        {
            var returnValue = new List<Trainer>()
            {
                new Trainer()
                {
                    Id = 1,
                    User = new AppUser()
                    {
                        Nickname = "asd",
                        Address = "adr",
                        PhoneNumber = "2232",
                    },
                    SchoolId = 1,
                    CoursesInvolved = new List<Course>(),
                    IsAvailable = true
                },
                new Trainer()
                {
                    Id = 2,
                    User = new AppUser()
                    {
                        Nickname = "asd2",
                        Address = "adr2",
                        PhoneNumber = "22322",
                    },
                    SchoolId = 2,
                    CoursesInvolved = new List<Course>(),
                    IsAvailable = false
                },
                new Trainer()
                {
                    Id = 3,
                    User = new AppUser()
                    {
                        Nickname = "asd3",
                        Address = "adr3",
                        PhoneNumber = "22323",
                    },
                    SchoolId = 1,
                    CoursesInvolved = new List<Course>()
                    {
                        new Course()
                    },
                    IsAvailable = true
                },
                new Trainer()
                {
                    Id = 4,
                    User = new AppUser()
                    {
                        Nickname = "asd4",
                        Address = "adr4",
                        PhoneNumber = "22324",
                    },
                    SchoolId = 1,
                    CoursesInvolved = new List<Course>(),
                    IsAvailable = false
                },
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.trainerService.AvailableTrainersBySchoolIdNotParticipateInCourse<AllTrainerViewModel>(1).ToList();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<List<AllTrainerViewModel>>());
        }

         [Test]
        public void AvailableTrainersBySchoolId_TrainersInDb_ReturnsTrainersWithSchoolIdAvailable()
        {
            var returnValue = new List<Trainer>()
            {
                new Trainer()
                {
                    Id = 1,
                    User = new AppUser()
                    {
                        Nickname = "asd",
                        Address = "adr",
                        PhoneNumber = "2232",
                    },
                    SchoolId = 1,
                    CoursesInvolved = new List<Course>(),
                    IsAvailable = true
                },
                new Trainer()
                {
                    Id = 2,
                    User = new AppUser()
                    {
                        Nickname = "asd2",
                        Address = "adr2",
                        PhoneNumber = "22322",
                    },
                    SchoolId = 2,
                    CoursesInvolved = new List<Course>(),
                    IsAvailable = false
                },
                new Trainer()
                {
                    Id = 3,
                    User = new AppUser()
                    {
                        Nickname = "asd3",
                        Address = "adr3",
                        PhoneNumber = "22323",
                    },
                    SchoolId = 1,
                    CoursesInvolved = new List<Course>()
                    {
                        new Course()
                    },
                    IsAvailable = true
                },
                new Trainer()
                {
                    Id = 4,
                    User = new AppUser()
                    {
                        Nickname = "asd4",
                        Address = "adr4",
                        PhoneNumber = "22324",
                    },
                    SchoolId = 1,
                    CoursesInvolved = new List<Course>(),
                    IsAvailable = false
                },
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.trainerService.AvailableTrainersBySchoolId<AllTrainerViewModel>(1).ToList();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result, Is.TypeOf<List<AllTrainerViewModel>>());
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
