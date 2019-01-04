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
    using Moq;
    using NUnit.Framework;

    public class SchoolServiceTests
    {
        private Mock<IRepository<School>> repository;
        private SchoolService schoolService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<School>>();
            this.schoolService = new SchoolService(this.repository.Object);
            this.SetMapper();
        }

        [Test]
        public void AllActiveSchools_NoSchoolsInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<School>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.schoolService.AllActiveSchools<AllSchoolViewModel>();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void AllActiveSchools_SchoolsInDb_ReturnsAListWithSchools()
        {
            var returnValue = new List<School>()
            {
                new School()
                {
                    IsActive = true
                },
                new School()
                {
                    IsActive = false
                },
                new School()
                {
                    IsActive = true
                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.schoolService.AllActiveSchools<AllSchoolViewModel>().Count();

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Create_ValidInputModel_ReturnsCreatedSchool()
        {
            var school = new School()
            {
                ManagerId = "aaaa",
                OfficeAddress = "asdfg",
                TradeMark = "testy",
                Phone = "222222"
            };

            this.repository.Setup(r => r.AddAsync(school)).Returns(Task.FromResult(school));

            var model = new CreateSchoolInputModel()
            {
                ManagerId = "aaaa",
                OfficeAddress = "asdfg",
                TradeMark = "testy",
                Phone = "222222"
            };

            var result = this.schoolService.Create(model);

            Assert.That(result.Id, Is.EqualTo(0));
            Assert.That(result.TradeMark, Is.EqualTo("testy"));
            Assert.That(result, Is.TypeOf<School>());
        }

        [Test]
        public void Delete_WithValidId_ReturnsDeletedSchool()
        {
            var school = new School()
            {
                Id = 1,
                ManagerId = "aaaa",
                OfficeAddress = "asdfg",
                TradeMark = "testy",
                Phone = "222222",
                IsActive = true
            };

            var returnValue = new List<School>()
            {
                school
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            this.repository.Setup(r => r.Update(school)).Callback(() => new School()
            {
                Id = 1,
                ManagerId = "aaaa",
                OfficeAddress = "asdfg",
                TradeMark = "testy",
                Phone = "222222",
                IsActive = true
            });

            var result = this.schoolService.Delete(1);

            Assert.That(result.IsActive, Is.False);
        }

        [Test]
        public void Edit_WithValidId_ReturnsEditedSchool()
        {
            var school = new School()
            {
                Id = 1,
                ManagerId = "aaaa",
                OfficeAddress = "asdfg",
                TradeMark = "testy",
                Phone = "222222",
                IsActive = true
            };

            var returnValue = new List<School>()
            {
                school
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            this.repository.Setup(r => r.Update(school)).Callback(() => new School()
            {
                Id = 1,
                ManagerId = "aaaa",
                OfficeAddress = "asdfg",
                TradeMark = "testy",
                Phone = "222222",
                IsActive = true
            });

            var model = new EditSchoolInputModel()
            {
                Id = 1,
                IsActive = false,
                Manager = new AppUser(),
                OfficeAddress = "bbbb",
                Phone = "1111111",
                TradeMark = "qwerty"
            };

            var result = this.schoolService.Edit(model);

            Assert.That(result.OfficeAddress, Is.EqualTo("bbbb"));
            Assert.That(result.IsActive, Is.EqualTo(false));
            Assert.That(result.Phone, Is.EqualTo("1111111"));
            Assert.That(result.TradeMark, Is.EqualTo("qwerty"));

        }

        [Test]
        public void ChangeManager_WithValidObject_ReturnsEditedSchool()
        {
            var school = new School()
            {
                Id = 1,
                ManagerId = "aaaa",
                OfficeAddress = "asdfg",
                TradeMark = "testy",
                Phone = "222222",
                IsActive = true
            };

            var returnValue = new List<School>()
            {
                school
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            this.repository.Setup(r => r.Update(school)).Callback(() => new School()
            {
                Id = 1,
                ManagerId = "aaaa",
                OfficeAddress = "asdfg",
                TradeMark = "testy",
                Phone = "222222",
                IsActive = true
            });

            var result = this.schoolService.ChangeManager(1, "bbbb");

            Assert.That(result.ManagerId, Is.EqualTo("bbbb"));
            Assert.That(result, Is.TypeOf<School>());

        }

        [Test]
        public void GenericGetSchoolById_SchoolsInDb_ReturnsSchoolWithId()
        {
            var returnValue = new List<School>()
            {
                new School
                {
                    Id = 1,
                    Manager = new AppUser()
                    {
                        UserName = "ManName1"
                    }
                },
                new School
                {
                    Id = 2,
                    Manager = new AppUser()
                    {
                        UserName = "ManName1"
                    }
                },
                new School
                {
                    Id = 3,
                    Manager = new AppUser()
                    {
                        UserName = "ManName1"
                    }
                },
                new School
                {
                    Id = 4,
                    Manager = new AppUser()
                    {
                        UserName = "ManName1"
                    }
                },
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.schoolService.GetSchoolById<AllSchoolViewModel>(2);

            Assert.That(result.Id, Is.EqualTo(2));
            Assert.That(result, Is.TypeOf<AllSchoolViewModel>());
        }

        [Test]
        public void GenericGetSchoolById_NoSchoolWithIdInDb_ReturnsNull()
        {
            var returnValue = new List<School>()
            {
                new School
                {
                    Id = 1,
                    Manager = new AppUser()
                    {
                        UserName = "ManName1"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
                new School
                {
                    Id = 2,
                    Manager = new AppUser()
                    {
                        UserName = "ManName2"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
                new School
                {
                    Id = 3,
                    Manager = new AppUser()
                    {
                        UserName = "ManName3"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
                new School
                {
                    Id = 4,
                    Manager = new AppUser()
                    {
                        UserName = "ManName1"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.schoolService.GetSchoolById<AllSchoolViewModel>(5);

            Assert.That(result,Is.Null);
        }

        [Test]
        public void GenericGetSchoolById_NoSchoolsInDb_ReturnsNull()
        {
            var returnValue = new List<School>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.schoolService.GetSchoolById<AllSchoolViewModel>(5);
            Assert.That(result,Is.Null);
        }

         [Test]
        public void GenericGetSchoolByManagerName_SchoolsInDb_ReturnsSchoolWithManagerName()
        {
            var returnValue = new List<School>()
            {
                new School
                {
                    Id = 1,
                    Manager = new AppUser()
                    {
                        UserName = "ManName1"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
                new School
                {
                    Id = 2,
                    Manager = new AppUser()
                    {
                        UserName = "ManName1"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
                new School
                {
                    Id = 3,
                    Manager = new AppUser()
                    {
                        UserName = "ManName2"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
                new School
                {
                    Id = 4,
                    Manager = new AppUser()
                    {
                        UserName = "ManName3"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.schoolService.GetSchoolByManagerName<AllSchoolViewModel>("ManName1");

            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<AllSchoolViewModel>());
        }

        [Test]
        public void GenericGetSchoolByManagerName_NoSchoolWithIdInDb_ThrowsException()
        {
            var returnValue = new List<School>()
            {
                new School
                {
                    Id = 1,
                    Manager = new AppUser()
                    {
                        UserName = "ManName1"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
                new School
                {
                    Id = 2,
                    Manager = new AppUser()
                    {
                        UserName = "ManName2"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
                new School
                {
                    Id = 3,
                    Manager = new AppUser()
                    {
                        UserName = "ManName3"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
                new School
                {
                    Id = 4,
                    Manager = new AppUser()
                    {
                        UserName = "ManName1"
                    },
                    TradeMark = "aaa",
                    OfficeAddress = "asdf22",
                    Phone = "1111"
                },
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

           Assert.That(()=>this.schoolService.GetSchoolByManagerName<AllSchoolViewModel>("ManName"),Throws.ArgumentException);
        }

        [Test]
        public void GenericGetSchoolByManagerName_NoSchoolsInDb_ReturnsNull()
        {
            var returnValue = new List<School>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

          Assert.That(()=>this.schoolService.GetSchoolByManagerName<AllSchoolViewModel>("ManName1"),Throws.ArgumentException);
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
