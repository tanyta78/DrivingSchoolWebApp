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
    using Models.Lesson;
    using Moq;
    using NUnit.Framework;
    using NUnit.Framework.Internal;

    [TestFixture]
    public class LessonServiceTests
    {
        private Mock<IRepository<Lesson>> repository;
        private LessonService lessonService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<Lesson>>();
            this.lessonService = new LessonService(this.repository.Object);
            this.SetMapper();
        }

        [Test]
        public void All_NoLessonsInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Lesson>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.lessonService.All();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void All_LessonsInDb_ReturnsAListWithLessons()
        {
            var returnValue = new List<Lesson>()
            {
                new Lesson(),
                new Lesson(),
                new Lesson()
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.lessonService.All().Count();

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void GenericGetLessonById_LessonsInDb_ReturnsLessonWithId()
        {
            var returnValue = new List<Lesson>()
           {
               new Lesson()
               {
                   Id = 1,
                   OrderId = 1,
                   Status = LessonStatus.Scheduled,
                   StartTime = new DateTime(2018,12,11,12,00,00),
                   EndTime = new DateTime(2018,12,11,13,0,0),
                   ThemeColor = "green",
                   IsFullDay = false,
                   Subject = "first lesson",
                   Description = "asdfg"
               },
               new Lesson()
               {
                   Id = 2,
                   OrderId = 1,
                   Status = LessonStatus.Scheduled,
                   StartTime = new DateTime(2018,12,12,12,00,00),
                   EndTime = new DateTime(2018,12,12,13,0,0),
                   ThemeColor = "green",
                   IsFullDay = false,
                   Subject = "second lesson",
                   Description = "asdfg"
               }
           };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.lessonService.GetLessonById<FullCalendarInputModel>(2);

            Assert.That(result.Id, Is.EqualTo(2));
        }

        [Test]
        public void GenericGetLessonById_NoLessonsInDb_ThrowsException()
        {
            var returnValue = new List<Lesson>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.lessonService.GetLessonById<FullCalendarInputModel>(5), Throws.ArgumentException);
        }

        [Test]
        public void GenericGetLessonsByCourseIdAndCustomerId_LessonsInDb_ReturnsLessonsWithCriteria()
        {
            var returnValue = new List<Lesson>()
            {
                new Lesson()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,11,12,00,00),
                    EndTime = new DateTime(2018,12,11,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 2,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,12,12,00,00),
                    EndTime = new DateTime(2018,12,12,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 3,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,13,12,00,00),
                    EndTime = new DateTime(2018,12,13,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 4,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,14,12,00,00),
                    EndTime = new DateTime(2018,12,14,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.lessonService.GetLessonsByCourseIdAndCustomerId<FullCalendarInputModel>(1, 1);

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GenericGetLessonsByCourseIdAndCustomerId_NoLessonsWithCriteriaInDb_ReturnsEmptyList()
        {
            var returnValue = new List<Lesson>()
            {
                new Lesson()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,11,12,00,00),
                    EndTime = new DateTime(2018,12,11,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 2,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,12,12,00,00),
                    EndTime = new DateTime(2018,12,12,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 3,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,13,12,00,00),
                    EndTime = new DateTime(2018,12,13,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 4,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,14,12,00,00),
                    EndTime = new DateTime(2018,12,14,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.lessonService.GetLessonsByCourseIdAndCustomerId<FullCalendarInputModel>(1, 3);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GenericGetLessonsByOrderId_LessonsInDb_ReturnsLessonsWithCriteria()
        {
            var returnValue = new List<Lesson>()
            {
                new Lesson()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,11,12,00,00),
                    EndTime = new DateTime(2018,12,11,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 2,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,12,12,00,00),
                    EndTime = new DateTime(2018,12,12,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 3,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,13,12,00,00),
                    EndTime = new DateTime(2018,12,13,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 4,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,14,12,00,00),
                    EndTime = new DateTime(2018,12,14,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.lessonService.GetLessonsByOrderId<FullCalendarInputModel>(1);

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GenericGetLessonsByOrderId_NoLessonsWithCriteriaInDb_ReturnsEmptyList()
        {
            var returnValue = new List<Lesson>()
            {
                new Lesson()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,11,12,00,00),
                    EndTime = new DateTime(2018,12,11,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 2,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,12,12,00,00),
                    EndTime = new DateTime(2018,12,12,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 3,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,13,12,00,00),
                    EndTime = new DateTime(2018,12,13,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 4,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,14,12,00,00),
                    EndTime = new DateTime(2018,12,14,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.lessonService.GetLessonsByOrderId<FullCalendarInputModel>(3);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GenericGetLessonsByCustomerId_LessonsInDb_ReturnsLessonsWithCriteria()
        {
            var returnValue = new List<Lesson>()
            {
                new Lesson()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,11,12,00,00),
                    EndTime = new DateTime(2018,12,11,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 2,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,12,12,00,00),
                    EndTime = new DateTime(2018,12,12,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 3,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,13,12,00,00),
                    EndTime = new DateTime(2018,12,13,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 4,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,14,12,00,00),
                    EndTime = new DateTime(2018,12,14,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.lessonService.GetLessonsByCustomerId<FullCalendarInputModel>(1);

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GenericGetLessonsByCustomerId_NoLessonsWithCriteriaInDb_ReturnsEmptyList()
        {
            var returnValue = new List<Lesson>()
            {
                new Lesson()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,11,12,00,00),
                    EndTime = new DateTime(2018,12,11,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 2,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,12,12,00,00),
                    EndTime = new DateTime(2018,12,12,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 3,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,13,12,00,00),
                    EndTime = new DateTime(2018,12,13,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 4,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,14,12,00,00),
                    EndTime = new DateTime(2018,12,14,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.lessonService.GetLessonsByCustomerId<FullCalendarInputModel>(3);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GenericGetLessonsByTrainerId_LessonsInDb_ReturnsLessonsWithCriteria()
        {
            var returnValue = new List<Lesson>()
            {
                new Lesson()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1,
                        Course = new Course()
                        {
                            TrainerId = 1
                        }
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,11,12,00,00),
                    EndTime = new DateTime(2018,12,11,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 2,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1,
                        Course = new Course()
                        {
                        TrainerId = 1
                        }
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,12,12,00,00),
                    EndTime = new DateTime(2018,12,12,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 3,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1,
                        Course = new Course()
                        {
                            TrainerId = 1
                        }
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,13,12,00,00),
                    EndTime = new DateTime(2018,12,13,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 4,
                    OrderId = 3,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 2,
                        Course = new Course()
                        {
                            TrainerId = 2
                        }
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,14,12,00,00),
                    EndTime = new DateTime(2018,12,14,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.lessonService.GetLessonsByTrainerId<FullCalendarInputModel>(1);

            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GenericGetLessonsByTrainerId_NoLessonsWithCriteriaInDb_ReturnsEmptyList()
        {
            var returnValue = new List<Lesson>()
            {
                new Lesson()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1,
                        Course = new Course()
                        {
                            TrainerId = 1
                        }
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,11,12,00,00),
                    EndTime = new DateTime(2018,12,11,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 2,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        CourseId = 1,
                        Course = new Course()
                        {
                            TrainerId = 1
                        }
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,12,12,00,00),
                    EndTime = new DateTime(2018,12,12,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 3,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1,
                        Course = new Course()
                        {
                            TrainerId = 1
                        }
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,13,12,00,00),
                    EndTime = new DateTime(2018,12,13,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "first lesson",
                    Description = "asdfg"
                },
                new Lesson()
                {
                    Id = 4,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        CourseId = 1,
                        Course = new Course()
                        {
                            TrainerId = 1
                        }
                    },
                    Status = LessonStatus.Scheduled,
                    StartTime = new DateTime(2018,12,14,12,00,00),
                    EndTime = new DateTime(2018,12,14,13,0,0),
                    ThemeColor = "green",
                    IsFullDay = false,
                    Subject = "second lesson",
                    Description = "asdfg"
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.lessonService.GetLessonsByTrainerId<FullCalendarInputModel>(3);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Create_ValidInputModel_ReturnsCreatedLesson()
        {
            var lesson = new Lesson()
            {
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "asdfg"
            };

            this.repository.Setup(r => r.AddAsync(lesson)).Returns(Task.FromResult(lesson));

            var model = new CreateLessonInputModel()
            {
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "asdfg"
            };

            var result = this.lessonService.Create(model).GetAwaiter().GetResult();

            Assert.That(result, Is.TypeOf<Lesson>());
            Assert.That(result.OrderId, Is.EqualTo(1));

        }

        [Test]
        public void Edit_ValidInputModel_ReturnsEditeddLesson()
        {
            var lesson = new Lesson()
            {
                Id = 1,
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "asdfg"
            };
            var returnValue = new List<Lesson>()
            {
                lesson
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);
            this.repository.Setup(r => r.Update(lesson)).Callback(() => new Lesson()
            {
                Id = 1,
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "new description"
            });

            var model = new EditLessonInputModel()
            {
                Id = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Description = "new description",
                Status = LessonStatus.Canceled
            };

            var result = this.lessonService.Edit(model).GetAwaiter().GetResult();

            Assert.That(result, Is.TypeOf<Lesson>());
            Assert.That(result.Description, Is.EqualTo("new description"));

        }

        [Test]
        public void Delete_WithValidId_ReturnsExam()
        {
            var lesson = new Lesson()
            {
                Id = 1,
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "asdfg"
            };

            var returnValue = new List<Lesson>()
            {
                lesson
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);
            this.repository.Setup(r => r.Delete(lesson)).Callback(() => new Lesson()
            {
                Id = 1,
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "asdfg"
            });

            var result = this.lessonService.Delete(1).GetAwaiter().GetResult();

            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test]
        public void Save_ValidInputModelWithIdEqualToZero_ReturnsCreatedLessonId()
        {
            var lesson = new Lesson()
            {
                Id = 0,
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "asdfg"
            };
            var returnValue = new List<Lesson>()
            {
                lesson
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);
            this.repository.Setup(r => r.Update(lesson)).Callback(() => new Lesson()
            {
                Id = 1,
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "new description"
            });
            this.repository.Setup(r => r.AddAsync(lesson)).Returns(Task.FromResult(lesson));

            var model = new FullCalendarInputModel()
            {
                Id = 0,
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "asdfg"
            };

            var result = this.lessonService.Save(model);

           Assert.That(result, Is.EqualTo(0));

        }

        [Test]
        public void Save_ValidInputModelWithIdNotEqualToZero_ReturnsEditedLessonId()
        {
            var lesson = new Lesson()
            {
                Id = 1,
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "asdfg"
            };
            var returnValue = new List<Lesson>()
            {
                lesson
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);
            this.repository.Setup(r => r.Update(lesson)).Callback(() => new Lesson()
            {
                Id = 1,
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "new description"
            });
            this.repository.Setup(r => r.AddAsync(lesson)).Returns(Task.FromResult(lesson));

            var model = new FullCalendarInputModel()
            {
                Id = 1,
                OrderId = 1,
                StartTime = new DateTime(2018, 12, 11, 12, 00, 00),
                EndTime = new DateTime(2018, 12, 11, 13, 0, 0),
                ThemeColor = "green",
                IsFullDay = false,
                Subject = "first lesson",
                Description = "new description"

            };

            var result = this.lessonService.Save(model);

            Assert.That(result, Is.EqualTo(1));

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
