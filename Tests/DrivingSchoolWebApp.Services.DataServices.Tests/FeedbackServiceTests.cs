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
    using Models.Feedback;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class FeedbackServiceTests
    {
        private Mock<IRepository<Feedback>> repository;
        private FeedbackService feedbackService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<Feedback>>();
            this.feedbackService = new FeedbackService(this.repository.Object);
            this.SetMapper();
        }

        [Test]
        public void All_NoFeedbackInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Feedback>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.feedbackService.All();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void All_FeedbackInDb_ReturnsAListWithGivenFeedback()
        {
            var returnValue = new List<Feedback>()
            {
                new Feedback(),
                new Feedback(),
                new Feedback()
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.feedbackService.All().Count();

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void GenericAll_NoFeedbackInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Feedback>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.feedbackService.All<AllFeedbackViewModel>();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GenericAll_feedbackInDb_ReturnsAListWithFeedback()
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

            var returnValue = new List<Feedback>()
            {
                new Feedback()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code1",
                    Rating = 1
                  },
                new Feedback()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code2",
                    Rating = 10
                },
                new Feedback()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Content = "test sample code3",
                    Rating = 9
                },
                new Feedback() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code4",
                    Rating = 10
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.feedbackService.All<AllFeedbackViewModel>().Count();

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void GenericGetFeedbackById_FeedbackInDb_ReturnsFeedbackWithId()
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

            var returnValue = new List<Feedback>()
            {
                new Feedback()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code1",
                    Rating = 1
                  },
                new Feedback()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code2",
                    Rating = 10
                },
                new Feedback()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Content = "test sample code3",
                    Rating = 9
                },
                new Feedback() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code4",
                    Rating = 10
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.feedbackService.GetFeedbackById<AllFeedbackViewModel>(3);

            Assert.That(result.Content, Is.EqualTo("test sample code3"));
        }

        [Test]
        public void GenericGetFeedbackById_NoFeedbackWithIdInDb_ThrowsException()
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

            var returnValue = new List<Feedback>()
            {
                new Feedback()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code1",
                    Rating = 1
                  },
                new Feedback()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code2",
                    Rating = 10
                },
                new Feedback()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Content = "test sample code3",
                    Rating = 9
                },
                new Feedback() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code4",
                    Rating = 10
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.feedbackService.GetFeedbackById<AllFeedbackViewModel>(5), Throws.ArgumentException);
        }

        [Test]
        public void GetFeedbackByCustomerId_NoFeedbackInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Feedback>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.feedbackService.GetFeedbacksByCustomerId<AllFeedbackViewModel>(5);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetFeedbackByCustomerId_feedbackInDb_ReturnsAListWithFeedback()
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

            var returnValue = new List<Feedback>()
            {
                new Feedback()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code1",
                    Rating = 1
                  },
                new Feedback()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code2",
                    Rating = 10
                },
                new Feedback()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Content = "test sample code3",
                    Rating = 9
                },
                new Feedback() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code4",
                    Rating = 10
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.feedbackService.GetFeedbacksByCustomerId<AllFeedbackViewModel>(1).Count();

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void GetFeedbackByCourseId_NoFeedbackInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Feedback>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.feedbackService.GetFeedbacksByCourseId<AllFeedbackViewModel>(5);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetFeedbackByCourseId_feedbackInDb_ReturnsAListWithFeedback()
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

            var returnValue = new List<Feedback>()
            {
                new Feedback()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code1",
                    Rating = 1
                  },
                new Feedback()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code2",
                    Rating = 10
                },
                new Feedback()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Content = "test sample code3",
                    Rating = 9
                },
                new Feedback() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code4",
                    Rating = 10
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.feedbackService.GetFeedbacksByCourseId<AllFeedbackViewModel>(1).Count();

            Assert.That(result, Is.EqualTo(3));
        }


        [Test]
        public void GetFeedbackBySchoolId_NoFeedbackInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Feedback>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.feedbackService.GetFeedbacksByCustomerId<AllFeedbackViewModel>(5);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetFeedbackBySchoolId_feedbackInDb_ReturnsAListWithFeedback()
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

            var returnValue = new List<Feedback>()
            {
                new Feedback()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code1",
                    Rating = 1
                  },
                new Feedback()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code2",
                    Rating = 10
                },
                new Feedback()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    Content = "test sample code3",
                    Rating = 9
                },
                new Feedback() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    Content = "test sample code4",
                    Rating = 10
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.feedbackService.GetFeedbacksBySchoolId<AllFeedbackViewModel>(1).Count();

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void Create_ValidInputModel_ReturnsCreatedFeedback()
        {
            var feedback = new Feedback()
            {
                CourseId = 1,
                CustomerId = 2
            };

            this.repository.Setup(r => r.AddAsync(feedback)).Returns(Task.FromResult(feedback));

            var model = new CreateFeedbackInputModel()
            {
                CourseId = 1,
                CustomerId = 2,
                Content = "asdfg",
                Rating = 10
            };

            var result = this.feedbackService.Create(model).GetAwaiter().GetResult();

            Assert.That(result.CourseId, Is.EqualTo(1));
            Assert.That(result.CustomerId, Is.EqualTo(2));

        }

        [Test]
        public void Delete_WithValidId_ReturnsFeedback()
        {
            var feedback = new Feedback()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                Content = "asdf",
                Rating = 10
            };

            var returnValue = new List<Feedback>()
            {
                feedback
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);
            this.repository.Setup(r => r.Delete(feedback)).Callback(() => new Feedback()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                Content = "asdf",
                Rating = 10
            });

            var result = this.feedbackService.Delete(1).GetAwaiter().GetResult();

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
