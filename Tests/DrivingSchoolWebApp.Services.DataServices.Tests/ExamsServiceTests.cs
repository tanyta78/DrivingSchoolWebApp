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
    using Models.Exam;
    using Moq;
    using NUnit.Framework;

    [TestFixture()]
    public class ExamsServiceTests
    {
        private Mock<IRepository<Exam>> repository;
        private ExamService examsService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<Exam>>();
            this.examsService = new ExamService(this.repository.Object);
            this.SetMapper();
        }

        [Test]
        public void All_NoExamsInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Exam>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.All();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void All_ExamsInDb_ReturnsAListWithExams()
        {
            var returnValue = new List<Exam>()
            {
                new Exam(),
                new Exam(),
                new Exam()
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.All().Count();

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void GenericAll_NoExamsInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Exam>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.All<AllExamsViewModel>();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GenericAll_ExamsInDb_ReturnsAListWithExams()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.All<AllExamsViewModel>().Count();

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void GenericGetExamById_ExamsInDb_ReturnsExamWithId()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamById<AllExamsViewModel>(2);

            Assert.That(result.Id, Is.EqualTo(2));
        }

        [Test]
        public void GenericGetExamById_NoExamWithIdInDb_ThrowsException()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.examsService.GetExamById<AllExamsViewModel>(5), Throws.ArgumentException);
        }

        [Test]
        public void GetExamById_ExamsInDb_ReturnsExamWithId()
        {
            var returnValue = new List<Exam>()
            {
                new Exam(){Id = 1},
                new Exam(){Id = 2},
                new Exam(){Id = 3},
                new Exam(){Id = 4}

            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamById(3);

            Assert.That(result.Id, Is.EqualTo(3));
        }

         [Test]
        public void GetExamById_NoExamWithIdInDb_ThrowsException()
        {
            var returnValue = new List<Exam>()
            {
                new Exam(){Id = 1},
                new Exam(){Id = 2},
                new Exam(){Id = 3},
                new Exam(){Id = 4}

            };
           
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.examsService.GetExamById(5), Throws.ArgumentException);
        }

        [Test]
        public void GenericGetExamByCustomerId_ExamsInDb_ReturnsExamWithId()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamsByCustomerId<AllExamsViewModel>(1);

            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GenericGetExamByCustomerId_NoExamWithCustomerIdInDb_ReturnsEmptyList()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamsByCustomerId<AllExamsViewModel>(5);
            Assert.That(result,Is.Empty);
        }

         [Test]
        public void GenericGetExamBySchoolId_ExamsInDb_ReturnsExamWithId()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamsBySchoolId<AllExamsViewModel>(1);

            Assert.That(result.Count(), Is.EqualTo(4));
        }

        [Test]
        public void GenericGetExamBySchoolId_NoExamWithCustomerIdInDb_ReturnsEmptyList()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamsBySchoolId<AllExamsViewModel>(5);
            Assert.That(result,Is.Empty);
        }

          [Test]
        public void GenericGetExamByCourseId_ExamsInDb_ReturnsExamWithId()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamsByCourseId<AllExamsViewModel>(1);

            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GenericGetExamByCourseId_NoExamWithCustomerIdInDb_ReturnsEmptyList()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamsByCourseId<AllExamsViewModel>(5);
            Assert.That(result,Is.Empty);
        }

          [Test]
        public void GenericGetExamByStatus_ExamsInDb_ReturnsExamsWithStatus()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Canceled,
                    Type = ExamType.Inside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamsByStatus<AllExamsViewModel>(LessonStatus.Canceled);

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GenericGetExamByStatus_NoExamWithCustomerIdInDb_ReturnsEmptyList()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamsByStatus<AllExamsViewModel>(LessonStatus.Canceled);
            Assert.That(result,Is.Empty);
        }

          [Test]
        public void GenericGetExamByType_ExamsInDb_ReturnsExamsWithType()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Canceled,
                    Type = ExamType.Inside
                },
                new Exam() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,11,11,22,34),
                    Date = new DateTime(2018,12,11),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Outside
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamsByType<AllExamsViewModel>(ExamType.Inside);

            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GenericGetExamByType_NoExamWithCustomerIdInDb_ReturnsEmptyList()
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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
                    TradeMark = "BestSchool",
                    ManagerId = "aaaa"
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

            var returnValue = new List<Exam>()
            {
                new Exam()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,11,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
                new Exam()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Time = new DateTime(2018,12,1,12,22,34),
                    Date = new DateTime(2018,12,1),
                    Status = LessonStatus.Scheduled,
                    Type = ExamType.Inside
                },
               
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.examsService.GetExamsByType<AllExamsViewModel>(ExamType.Outside);
            Assert.That(result,Is.Empty);
        }

        [Test]
        public void Create_ValidInputModel_ReturnsCreatedExam()
        {
            var exam = new Exam()
            {
                CourseId = 1,
                CustomerId = 2
            };

            this.repository.Setup(r => r.AddAsync(exam)).Returns(Task.FromResult(exam));

            var model = new CreateExamInputModel()
            {
                CourseId = 1,
                CustomerId = 2
            };

            var result = this.examsService.Create(model).GetAwaiter().GetResult();

            Assert.That(result.CourseId, Is.EqualTo(1));
            Assert.That(result.CustomerId, Is.EqualTo(2));

        }

        [Test]
        public void CancelExam_WithValidObject_ReturnsExam()
        {
            var exam = new Exam()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                Status = LessonStatus.Scheduled
            };

            var returnValue = new List<Exam>()
            {
                exam
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            this.repository.Setup(r => r.Update(exam)).Callback(() => new Exam()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                Status = LessonStatus.Canceled
            });

          var result = this.examsService.CancelExam(1).GetAwaiter().GetResult();

          
            Assert.That(result.Status, Is.EqualTo(LessonStatus.Canceled));
            Assert.That(result,Is.TypeOf<Exam>());
        }

        [Test]
        public void ChangeStatus_WithValidObject_ReturnsExam()
        {
            var exam = new Exam()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                Status = LessonStatus.Scheduled
            };

            var returnValue = new List<Exam>()
            {
                exam
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            this.repository.Setup(r => r.Update(exam)).Callback(() => new Exam()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                Status = LessonStatus.Finished
            });

            var result = this.examsService.ChangeStatus(1,LessonStatus.Finished).GetAwaiter().GetResult();

          
            Assert.That(result.Status, Is.EqualTo(LessonStatus.Finished));
            Assert.That(result,Is.TypeOf<Exam>());
        }

        [Test]
        public void Delete_WithValidId_ReturnsExam()
        {
            var exam = new Exam()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                Status = LessonStatus.Scheduled
            };

            var returnValue = new List<Exam>()
            {
                exam
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);
            this.repository.Setup(r => r.Delete(exam)).Callback(() => new Exam()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                Status = LessonStatus.Scheduled
            });

           var result = this.examsService.Delete(1).GetAwaiter().GetResult();

            Assert.That(result.Id, Is.EqualTo(1));
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
