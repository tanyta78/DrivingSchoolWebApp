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
    using Models.Customer;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class CustomerServiceTests
    {
        private Mock<IRepository<Customer>> repository;
        private CustomerService customerService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<Customer>>();
            this.customerService = new CustomerService(this.repository.Object);
            this.SetMapper();
        }

        [Test]
        public void All_NoCustomersInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Customer>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.customerService.All();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void All_WhenCall_ReturnsAllCustomers()
        {
            var returnValue = new List<Customer>
            {
                new Customer()
                {
                   Id = 1,

                },
                new Customer()
                {
                    Id = 2,

                },
                new Customer()
                {
                    Id = 3,

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            var result = this.customerService.All();

            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public void GenericAll_WhenCall_ReturnsAllCustomersAsListOfGenericType()
        {
            var returnValue = new List<Customer>
            {
                new Customer()
                {
                  Id = 1,
                  User = new AppUser(),
                  Gender = Gender.Female,
                  AgeGroup = AgeGroup.Adult,
                  EducationLevel = EducationLevel.Master,
                  CoursesOrdered = new List<Order>(),
                  ExamsTaken = new List<Exam>(),
                  Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 2,
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.YoungAdult,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 3,
                    User = new AppUser(),
                    Gender = Gender.Male,
                    AgeGroup = AgeGroup.YoungSeniorCitizen,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            var result = this.customerService.All<CustomerViewModel>().ToList();

            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result, Is.TypeOf<List<CustomerViewModel>>());
        }

        [Test]
        public void Create_ValidInputModel_ReturnsCreatedCustomer()
        {
            var customer = new Customer()
            {
                Id = 1,
                UserId = "aaaa",
                User = new AppUser(),
                Gender = Gender.Female,
                AgeGroup = AgeGroup.Adult,
                EducationLevel = EducationLevel.Master,
                CoursesOrdered = new List<Order>(),
                ExamsTaken = new List<Exam>(),
                Feedbacks = new List<Feedback>()
            };

            this.repository.Setup(r => r.AddAsync(customer)).Returns(Task.FromResult(customer));

            var model = new CreateCustomerInputModel()
            {
                UserId = "aaaa",
                Gender = Gender.Female,
                AgeGroup = AgeGroup.Adult,
                EducationLevel = EducationLevel.Master,
            };

            var result = this.customerService.Create(model);

            Assert.That(result.UserId, Is.EqualTo("aaaa"));
            Assert.That(result.Gender, Is.EqualTo(Gender.Female));
            Assert.That(result, Is.TypeOf<Customer>());
        }

        [Test]
        public void Delete_WithValidCustomerObject_ReturnsCustomer()
        {
            var customer = new Customer()
            {
                Id = 3,
                User = new AppUser(),
                Gender = Gender.Male,
                AgeGroup = AgeGroup.YoungSeniorCitizen,
                EducationLevel = EducationLevel.Bachelor,
                CoursesOrdered = new List<Order>(),
                ExamsTaken = new List<Exam>(),
                Feedbacks = new List<Feedback>()
            };

            var returnValue = new List<Customer>()
            {
                customer
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);
            this.repository.Setup(r => r.Delete(customer)).Callback(() => new Customer()
            {
                Id = 3,
                User = new AppUser(),
                Gender = Gender.Male,
                AgeGroup = AgeGroup.YoungSeniorCitizen,
                EducationLevel = EducationLevel.Bachelor,
                CoursesOrdered = new List<Order>(),
                ExamsTaken = new List<Exam>(),
                Feedbacks = new List<Feedback>()
            });

           var result = this.customerService.Delete(3);

            Assert.That(result.Gender, Is.EqualTo(Gender.Male));
            Assert.That(result, Is.TypeOf<Customer>());
        }

        [Test]
        public void GenericGetCustomerById_CustomersInDb_ReturnsCustomerWithId()
        {
            var returnValue = new List<Customer>
        {
            new Customer()
            {
                Id = 1,
                User = new AppUser(),
                Gender = Gender.Female,
                AgeGroup = AgeGroup.Adult,
                EducationLevel = EducationLevel.Master,
                CoursesOrdered = new List<Order>(),
                ExamsTaken = new List<Exam>(),
                Feedbacks = new List<Feedback>()
            },
            new Customer()
            {
                Id = 2,
                User = new AppUser(),
                Gender = Gender.Female,
                AgeGroup = AgeGroup.YoungAdult,
                EducationLevel = EducationLevel.Bachelor,
                CoursesOrdered = new List<Order>(),
                ExamsTaken = new List<Exam>(),
                Feedbacks = new List<Feedback>()
            },
            new Customer()
            {
                Id = 3,
                User = new AppUser(),
                Gender = Gender.Male,
                AgeGroup = AgeGroup.YoungSeniorCitizen,
                EducationLevel = EducationLevel.Bachelor,
                CoursesOrdered = new List<Order>(),
                ExamsTaken = new List<Exam>(),
                Feedbacks = new List<Feedback>()
            }
        };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.customerService.GetCustomerById<CustomerViewModel>(1);

            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Gender, Is.EqualTo(Gender.Female));
            Assert.That(result, Is.TypeOf<CustomerViewModel>());
        }

        [Test]
        public void GenericGetCustomerById_NoCustomerWithIdInDb_ThrowsException()
        {
            var returnValue = new List<Customer>
            {
                new Customer()
                {
                    Id = 1,
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.Adult,
                    EducationLevel = EducationLevel.Master,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 2,
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.YoungAdult,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 3,
                    User = new AppUser(),
                    Gender = Gender.Male,
                    AgeGroup = AgeGroup.YoungSeniorCitizen,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.customerService.GetCustomerById<CustomerViewModel>(4), Throws.ArgumentException);
        }
      
        [Test]
        public void GenericGetCustomerUserById_CustomersInDb_ReturnsCustomerWithUserId()
        {
            var returnValue = new List<Customer>
            {
                new Customer()
                {
                    Id = 1,
                    UserId = "aaaa",
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.Adult,
                    EducationLevel = EducationLevel.Master,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 2,
                    UserId = "aaaa2",
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.YoungAdult,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 3,
                    UserId = "aaaa3",
                    User = new AppUser(),
                    Gender = Gender.Male,
                    AgeGroup = AgeGroup.YoungSeniorCitizen,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.customerService.GetCustomerByUserId<CustomerViewModel>("aaaa");

            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Gender, Is.EqualTo(Gender.Female));
            Assert.That(result, Is.TypeOf<CustomerViewModel>());
        }

        [Test]
        public void GenericGetCustomerByUserId_NoCustomerWithUserIdInDb_ThrowsException()
        {
            var returnValue = new List<Customer>
            {
                new Customer()
                {
                    Id = 1,
                    UserId="aaaa",
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.Adult,
                    EducationLevel = EducationLevel.Master,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 2,
                    UserId="aaaa2",
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.YoungAdult,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 3,
                    UserId="aaaa3",
                    User = new AppUser(),
                    Gender = Gender.Male,
                    AgeGroup = AgeGroup.YoungSeniorCitizen,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.customerService.GetCustomerByUserId<CustomerViewModel>("aaa4"), Throws.ArgumentException);
        }

         [Test]
        public void GetCustomerByUserId_CustomersInDb_ReturnsCustomerWithUserId()
        {
            var returnValue = new List<Customer>
            {
                new Customer()
                {
                    Id = 1,
                    UserId = "aaaa",
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.Adult,
                    EducationLevel = EducationLevel.Master,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 2,
                    UserId = "aaaa2",
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.YoungAdult,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 3,
                    UserId = "aaaa3",
                    User = new AppUser(),
                    Gender = Gender.Male,
                    AgeGroup = AgeGroup.YoungSeniorCitizen,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.customerService.GetCustomerByUserId("aaaa");

            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Gender, Is.EqualTo(Gender.Female));
            Assert.That(result, Is.TypeOf<Customer>());
        }

        [Test]
        public void GetCustomerByUserId_NoCustomersWithUserIdInDb_ThrowsException()
        {
            var returnValue = new List<Customer>
            {
                new Customer()
                {
                    Id = 1,
                    UserId="aaaa",
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.Adult,
                    EducationLevel = EducationLevel.Master,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 2,
                    UserId="aaaa2",
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.YoungAdult,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 3,
                    UserId="aaaa3",
                    User = new AppUser(),
                    Gender = Gender.Male,
                    AgeGroup = AgeGroup.YoungSeniorCitizen,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.customerService.GetCustomerByUserId("aaaa4"), Throws.ArgumentException);
        }

        [Test]
        public void GetCustomerById_CustomersInDb_ReturnsCustomerWithId()
        {
            var returnValue = new List<Customer>
            {
                new Customer()
                {
                    Id = 1,
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.Adult,
                    EducationLevel = EducationLevel.Master,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 2,
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.YoungAdult,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 3,
                    User = new AppUser(),
                    Gender = Gender.Male,
                    AgeGroup = AgeGroup.YoungSeniorCitizen,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.customerService.GetCustomerById(1);

            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Gender, Is.EqualTo(Gender.Female));
            Assert.That(result, Is.TypeOf<Customer>());
        }
       
        [Test]
        public void GetCustomerById_NoCustomersWithIdInDb_ThrowsException()
        {
            var returnValue = new List<Customer>
            {
                new Customer()
                {
                    Id = 1,
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.Adult,
                    EducationLevel = EducationLevel.Master,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 2,
                    User = new AppUser(),
                    Gender = Gender.Female,
                    AgeGroup = AgeGroup.YoungAdult,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                },
                new Customer()
                {
                    Id = 3,
                    User = new AppUser(),
                    Gender = Gender.Male,
                    AgeGroup = AgeGroup.YoungSeniorCitizen,
                    EducationLevel = EducationLevel.Bachelor,
                    CoursesOrdered = new List<Order>(),
                    ExamsTaken = new List<Exam>(),
                    Feedbacks = new List<Feedback>()
                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.customerService.GetCustomerById(4), Throws.ArgumentException);
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
