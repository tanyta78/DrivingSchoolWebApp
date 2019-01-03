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
    using Models.Course;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class CourseServiceTests
    {
        private Mock<IRepository<Course>> repository;
        private CourseService courseService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<Course>>();
            this.courseService = new CourseService(this.repository.Object);
            this.SetMapper();
        }

        [Test]
        public void All_NoCoursesInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Course>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.courseService.All();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void All_WhenCall_ReturnsAllCoursesNotFinishedActiveSchoolAndApprovedManager()
        {
            var returnValue = new List<Course>
            {
                new Course()
                {
                   Id = 1,
                   Category = Category.C,
                   SchoolId = 1,
                   School = new School()
                   {
                       TradeMark = "Besty",
                       IsActive = true,
                       Manager = new AppUser()
                       {
                           IsApproved = true
                       }
                   },
                   Price = 200,
                   IsFinished = false
                },
                new Course()
                {
                    Id = 2,
                    Category = Category.C,
                    SchoolId = 2,
                    School = new School()
                    {
                        TradeMark = "Besty2",
                        IsActive = false,
                        Manager = new AppUser()
                        {
                            IsApproved = true
                        }
                    },
                    Price = 200,
                    IsFinished = false
                },
                new Course()
                {
                    Id = 3,
                    Category = Category.C,
                    SchoolId = 3,
                    School = new School()
                    {
                        TradeMark = "Besty3",
                        IsActive = true,
                        Manager = new AppUser()
                        {
                            IsApproved = false
                        }
                    },
                    Price = 200,
                    IsFinished = true
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            var result = this.courseService.All();

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GenericGetAllCourses_WhenCall_ReturnsAllCoursesNotFinishedActiveSchoolAndApprovedManager()
        {
            var returnValue = new List<Course>
            {
                new Course()
                {
                   Id = 1,
                   Category = Category.C,
                   SchoolId = 1,
                   School = new School()
                   {
                       TradeMark = "Besty",
                       IsActive = true,
                       Manager = new AppUser()
                       {
                           IsApproved = true
                       }
                   },
                   Price = 200,
                   IsFinished = false
                },
                new Course()
                {
                    Id = 2,
                    Category = Category.C,
                    SchoolId = 2,
                    School = new School()
                    {
                        TradeMark = "Besty2",
                        IsActive = false,
                        Manager = new AppUser()
                        {
                            IsApproved = true
                        }
                    },
                    Price = 200,
                    IsFinished = false
                },
                new Course()
                {
                    Id = 3,
                    Category = Category.C,
                    SchoolId = 3,
                    School = new School()
                    {
                        TradeMark = "Besty3",
                        IsActive = true,
                        Manager = new AppUser()
                        {
                            IsApproved = false
                        }
                    },
                    Price = 200,
                    IsFinished = true
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            var result = this.courseService.GetAllCourses<AllCoursesViewModel>().ToList();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<List<AllCoursesViewModel>>());
        }

        [Test]
        public void Create_ValidInputModel_ReturnsCreatedCourse()
        {
            var course = new Course()
            {
                Id = 1,
                Category = Category.A,
                Description = "TestDesc",
                MinimumLessonsCount = 20,
                Price = 100,
                TrainerId = 1,
                CarId = 1,
                SchoolId = 1
            };

            this.repository.Setup(r => r.AddAsync(course)).Returns(Task.FromResult(course));

            var model = new CreateCourseInputModel()
            {
                Category = Category.A,
                Description = "TestDesc",
                MinimumLessonsCount = 20,
                Price = 100,
                TrainerId = 1,
                CarId = 1,
                SchoolId = 1
            };

            var result = this.courseService.Create(model).GetAwaiter().GetResult();

            Assert.That(result.TrainerId, Is.EqualTo(1));
            Assert.That(result.Price, Is.EqualTo(100));
            Assert.That(result, Is.TypeOf<Course>());
        }

        [Test]
        public void Edit_WithValidCourseObject_ReturnsCourse()
        {
            var course = new Course()
            {
                Id = 1,
                Category = Category.A,
                Description = "TestDesc",
                MinimumLessonsCount = 20,
                Price = 100,

                TrainerId = 1,
                CarId = 1,
                SchoolId = 1
            };

            var returnValue = new List<Course>()
          {
              course
          };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            this.repository.Setup(r => r.Update(course)).Callback(() => new Course()
            {
                Id = 1,
                Category = Category.A,
                Description = "TestDescNew",
                MinimumLessonsCount = 40,
                Price = 200,
                TrainerId = 2,
                CarId = 2,
                SchoolId = 1
            });

            var model = new EditCourseInputModel()
            {
                Id = 1,
                Description = "TestDescNew",
                MinimumLessonsCount = 40,
                Price = 200,
                TrainerId = 2,
                CarId = 2,
            };

            var result = this.courseService.Edit(model).GetAwaiter().GetResult();

            Assert.That(result.Description, Is.EqualTo("TestDescNew"));
            Assert.That(result.MinimumLessonsCount, Is.EqualTo(40));
            Assert.That(result.Price, Is.EqualTo(200));
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.TrainerId, Is.EqualTo(2));
            Assert.That(result.CarId, Is.EqualTo(2));

            Assert.That(result.SchoolId, Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<Course>());
        }

        [Test]
        public void Delete_WithValidCourseObject_ReturnsCourse()
        {
            var course = new Course()
            {
                Id = 1,
                Category = Category.A,
                Description = "TestDesc",
                MinimumLessonsCount = 20,
                Price = 100,
                TrainerId = 1,
                CarId = 1,
                SchoolId = 1
            };

            var returnValue = new List<Course>()
            {
                course
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);
            this.repository.Setup(r => r.Delete(course)).Callback(() => new Course()
            {
                Id = 1,
                Category = Category.A,
                Description = "TestDesc",
                MinimumLessonsCount = 20,
                Price = 100,
                TrainerId = 1,
                CarId = 1,
                SchoolId = 1
            });

            var model = new DeleteCourseViewModel()
            {
                Id = 1
            };

            var result = this.courseService.Delete(1).GetAwaiter().GetResult();

            Assert.That(result.Price, Is.EqualTo(100));
            Assert.That(result, Is.TypeOf<Course>());
        }

        [Test]
        public void GetCourseById_CoursesInDb_ReturnsCourseWithId()
        {
            var returnValue = new List<Course>()
            {
               new Course()
                {
                Id = 1,
                Category = Category.A,
                Description = "TestDesc",
                MinimumLessonsCount = 20,
                Price = 100,
                TrainerId = 1,
                CarId = 1,
                SchoolId = 1
            },
                new Course(){Id = 2},
                new Course(){Id = 3}
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.courseService.GetCourseById<EditCourseInputModel>(1);

            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.TrainerId, Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<EditCourseInputModel>());
        }

        [Test]
        public void GetCourseById_NoCourseWihIdInDb_ThrowsException()
        {
            var returnValue = new List<Course>()
            {
                new Course()
                {
                    Id = 1,
                    Category = Category.A,
                    Description = "TestDesc",
                    MinimumLessonsCount = 20,
                    Price = 100,
                    TrainerId = 1,
                    CarId = 1,
                    SchoolId = 1
                },
                new Course(){Id = 2},
                new Course(){Id = 3}
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.courseService.GetCourseById<EditCourseInputModel>(4), Throws.ArgumentException);
        }

        [Test]
        public void GetCoursesByCarId_WithValidData_ReturnsCoursesWithWantedCarIdNotFinishedActiveSchoolAndApprovedManager()
        {
            var returnValue = new List<Course>
            {
                new Course()
                {
                   Id = 1,
                   Category = Category.C,
                   SchoolId = 1,
                   School = new School()
                   {
                       TradeMark = "Besty",
                       IsActive = true,
                       Manager = new AppUser()
                       {
                           IsApproved = true
                       }
                   },
                   Price = 200,
                   IsFinished = false,
                   CarId = 1
                },
                new Course()
                {
                    Id = 2,
                    Category = Category.C,
                    SchoolId = 2,
                    School = new School()
                    {
                        TradeMark = "Besty2",
                        IsActive = false,
                        Manager = new AppUser()
                        {
                            IsApproved = true
                        }
                    },
                    Price = 200,
                    IsFinished = false,
                    CarId = 1
                },
                new Course()
                {
                    Id = 3,
                    Category = Category.C,
                    SchoolId = 3,
                    School = new School()
                    {
                        TradeMark = "Besty3",
                        IsActive = true,
                        Manager = new AppUser()
                        {
                            IsApproved = false
                        }
                    },
                    Price = 200,
                    IsFinished = true,
                    CarId = 2
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            var result = this.courseService.GetCoursesByCarId<AllCoursesViewModel>(1).ToList();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<List<AllCoursesViewModel>>());

        }

         [Test]
        public void GetCoursesByCategoty_WithValidData_ReturnsCoursesWithWantedCategoryNotFinishedActiveSchoolAndApprovedManager()
        {
            var returnValue = new List<Course>
            {
                new Course()
                {
                   Id = 1,
                   Category = Category.C,
                   SchoolId = 1,
                   School = new School()
                   {
                       TradeMark = "Besty",
                       IsActive = true,
                       Manager = new AppUser()
                       {
                           IsApproved = true
                       }
                   },
                   Price = 200,
                   IsFinished = false,
                   CarId = 1
                },
                new Course()
                {
                    Id = 2,
                    Category = Category.C,
                    SchoolId = 2,
                    School = new School()
                    {
                        TradeMark = "Besty2",
                        IsActive = false,
                        Manager = new AppUser()
                        {
                            IsApproved = true
                        }
                    },
                    Price = 200,
                    IsFinished = false,
                    CarId = 1
                },
                new Course()
                {
                    Id = 3,
                    Category = Category.C,
                    SchoolId = 3,
                    School = new School()
                    {
                        TradeMark = "Besty3",
                        IsActive = true,
                        Manager = new AppUser()
                        {
                            IsApproved = false
                        }
                    },
                    Price = 200,
                    IsFinished = true,
                    CarId = 2
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            var result = this.courseService.GetCoursesByCategory<AllCoursesViewModel>(Category.C).ToList();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<List<AllCoursesViewModel>>());

        }

          [Test]
        public void GetCoursesBySchoolId_WithValidData_ReturnsCoursesWithWantedSchoolId()
        {
            var returnValue = new List<Course>
            {
                new Course()
                {
                   Id = 1,
                   Category = Category.C,
                   SchoolId = 1,
                   School = new School()
                   {
                       TradeMark = "Besty",
                       IsActive = true,
                       Manager = new AppUser()
                       {
                           IsApproved = true
                       }
                   },
                   Price = 200,
                   IsFinished = false,
                   CarId = 1
                },
                new Course()
                {
                    Id = 2,
                    Category = Category.C,
                    SchoolId = 2,
                    School = new School()
                    {
                        TradeMark = "Besty2",
                        IsActive = false,
                        Manager = new AppUser()
                        {
                            IsApproved = true
                        }
                    },
                    Price = 200,
                    IsFinished = false,
                    CarId = 1
                },
                new Course()
                {
                    Id = 3,
                    Category = Category.C,
                    SchoolId = 3,
                    School = new School()
                    {
                        TradeMark = "Besty3",
                        IsActive = true,
                        Manager = new AppUser()
                        {
                            IsApproved = false
                        }
                    },
                    Price = 200,
                    IsFinished = true,
                    CarId = 2
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            var result = this.courseService.GetCoursesBySchoolId<AllCoursesViewModel>(1).ToList();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<List<AllCoursesViewModel>>());

        }

         [Test]
        public void GetCoursesBySchoolIdAndCategory_WithValidData_ReturnsCoursesWithWantedSchoolIdAndCategory()
        {
            var returnValue = new List<Course>
            {
                new Course()
                {
                   Id = 1,
                   Category = Category.C,
                   SchoolId = 1,
                   School = new School()
                   {
                       TradeMark = "Besty",
                       IsActive = true,
                       Manager = new AppUser()
                       {
                           IsApproved = true
                       }
                   },
                   Price = 200,
                   IsFinished = false,
                   CarId = 1
                },
                new Course()
                {
                    Id = 2,
                    Category = Category.C,
                    SchoolId = 2,
                    School = new School()
                    {
                        TradeMark = "Besty2",
                        IsActive = false,
                        Manager = new AppUser()
                        {
                            IsApproved = true
                        }
                    },
                    Price = 200,
                    IsFinished = false,
                    CarId = 1
                },
                new Course()
                {
                    Id = 3,
                    Category = Category.C,
                    SchoolId = 3,
                    School = new School()
                    {
                        TradeMark = "Besty3",
                        IsActive = true,
                        Manager = new AppUser()
                        {
                            IsApproved = false
                        }
                    },
                    Price = 200,
                    IsFinished = true,
                    CarId = 2
                },
                new Course()
                {
                    Id = 1,
                    Category = Category.B,
                    SchoolId = 1,
                    School = new School()
                    {
                        TradeMark = "Besty",
                        IsActive = true,
                        Manager = new AppUser()
                        {
                            IsApproved = true
                        }
                    },
                    Price = 200,
                    IsFinished = false,
                    CarId = 1
                },
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            var result = this.courseService.GetCoursesBySchoolIdAndCategory<AllCoursesViewModel>(1,Category.B).ToList();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<List<AllCoursesViewModel>>());

        }

        
         [Test]
        public void GetCoursesByTrainerId_WithValidData_ReturnsCoursesWithWantedTrainerId()
        {
            var returnValue = new List<Course>
            {
                new Course()
                {
                   Id = 1,
                   Category = Category.C,
                   SchoolId = 1,
                   School = new School()
                   {
                       TradeMark = "Besty",
                       IsActive = true,
                       Manager = new AppUser()
                       {
                           IsApproved = true
                       }
                   },
                   Price = 200,
                   IsFinished = false,
                   CarId = 1,
                   TrainerId = 1
                },
                new Course()
                {
                    Id = 2,
                    Category = Category.C,
                    SchoolId = 2,
                    School = new School()
                    {
                        TradeMark = "Besty2",
                        IsActive = false,
                        Manager = new AppUser()
                        {
                            IsApproved = true
                        }
                    },
                    Price = 200,
                    IsFinished = false,
                    CarId = 1,
                    TrainerId = 1
                },
                new Course()
                {
                    Id = 3,
                    Category = Category.C,
                    SchoolId = 3,
                    School = new School()
                    {
                        TradeMark = "Besty3",
                        IsActive = true,
                        Manager = new AppUser()
                        {
                            IsApproved = false
                        }
                    },
                    Price = 200,
                    IsFinished = true,
                    CarId = 2,
                    TrainerId = 2
                },
                new Course()
                {
                    Id = 1,
                    Category = Category.B,
                    SchoolId = 1,
                    School = new School()
                    {
                        TradeMark = "Besty",
                        IsActive = true,
                        Manager = new AppUser()
                        {
                            IsApproved = true
                        }
                    },
                    Price = 200,
                    IsFinished = false,
                    CarId = 1,
                    TrainerId = 1
                },
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            var result = this.courseService.GetCoursesByTrainerId<AllCoursesViewModel>(1).ToList();

            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result, Is.TypeOf<List<AllCoursesViewModel>>());

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
