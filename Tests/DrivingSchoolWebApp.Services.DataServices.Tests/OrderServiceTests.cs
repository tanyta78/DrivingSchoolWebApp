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
    using Models.Order;
    using Moq;
    using NUnit.Framework;
    using NUnit.Framework.Internal;

    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IRepository<Order>> repository;
        private OrderService orderService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<Order>>();
            this.orderService = new OrderService(this.repository.Object);
            this.SetMapper();
        }

        [Test]
        public void All_NoOrdersInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Order>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.All();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void All_OrdersInDb_ReturnsAListWithOrders()
        {
            var returnValue = new List<Order>()
            {
                new Order(),
                new Order(),
                new Order()
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.All().Count();

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void GenericAll_NoOrdersInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Order>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.All<AllOrdersViewModel>();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GenericAll_OrdersInDb_ReturnsAListWithOrders()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.All<AllOrdersViewModel>().Count();

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void GenericGetOrderById_OrdersInDb_ReturnsOrderWithId()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrderById<AllOrdersViewModel>(2);

            Assert.That(result.Id, Is.EqualTo(2));
            Assert.That(result.CustomerId, Is.EqualTo(2));
            Assert.That(result, Is.TypeOf<AllOrdersViewModel>());
        }

        [Test]
        public void GenericGetOrderById_NoOrderWithIdInDb_ThrowsException()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };
          
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.orderService.GetOrderById<AllOrdersViewModel>(5), Throws.ArgumentException);
        }

        [Test]
        public void GenericGetOrderById_NoOrdersInDb_ThrowsException()
        {
            var returnValue = new List<Order>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.orderService.GetOrderById<AllOrdersViewModel>(5), Throws.ArgumentException);
        }

         [Test]
        public void GetOrdersByCustomerId_NoOrdersInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Order>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersByCustomerId<AllOrdersViewModel>(1);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetOrdersByCustomerId_OrdersInDb_ReturnsAListWithOrders()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersByCustomerId<AllOrdersViewModel>(1).Count();

            Assert.That(result, Is.EqualTo(3));
        }
        
       [Test]
        public void GetOrdersByCourseId_NoOrdersInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Order>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersByCourseId<AllOrdersViewModel>(1);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetOrdersByCourseId_OrdersInDb_ReturnsAListWithOrders()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersByCourseId<AllOrdersViewModel>(1).Count();

            Assert.That(result, Is.EqualTo(3));
        }

         [Test]
        public void GetOrdersByCourseId_NoOrdersWithCourseInDb_ReturnsEmptyList()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersByCourseId<AllOrdersViewModel>(3);

            Assert.That(result, Is.Empty);
        }

         [Test]
        public void GetOrdersBySchoolId_NoOrdersInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Order>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolId<AllOrdersViewModel>(1);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetOrdersBySchoolId_OrdersInDb_ReturnsAListWithOrders()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolId<AllOrdersViewModel>(1).Count();

            Assert.That(result, Is.EqualTo(4));
        }

         [Test]
        public void GetOrdersBySchoolId_NoOrdersWithSchoolInDb_ReturnsEmptyList()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolId<AllOrdersViewModel>(3);

            Assert.That(result, Is.Empty);
        }

         [Test]
        public void GetOrdersByStatus_NoOrdersInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Order>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersByStatus<AllOrdersViewModel>(OrderStatus.AwaitingPayment);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetOrdersByStatus_OrdersInDb_ReturnsAListWithOrders()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.Cancelled

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersByStatus<AllOrdersViewModel>(OrderStatus.AwaitingPayment).Count();

            Assert.That(result, Is.EqualTo(3));
        }

         [Test]
        public void GetOrdersByStatus_NoOrdersWithStatusDb_ReturnsEmptyList()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersByStatus<AllOrdersViewModel>(OrderStatus.Cancelled);

            Assert.That(result, Is.Empty);
        }

         [Test]
        public void GetOrdersBySchoolIdAndPaymentMade_NoOrdersInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Order>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolIdAndPaymentMade<AllOrdersViewModel>(1);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetOrdersBySchoolIdAndPaymentMade_OrdersInDb_ReturnsAListWithOrders()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.PaymentReceived
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.PaymentUpdated

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.Completed

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolIdAndPaymentMade<AllOrdersViewModel>(1).Count();

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void GetOrdersBySchoolIdAndPaymentMade_NoOrdersWithSchoolIdDb_ReturnsEmptyList()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.PaymentUpdated
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.PaymentReceived

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.PaymentUpdated

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.Completed

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolIdAndPaymentMade<AllOrdersViewModel>(3);

            Assert.That(result, Is.Empty);
        }

         [Test]
        public void GetOrdersBySchoolIdAndPaymentMade_NoOrdersWithPaymentMadeDb_ReturnsEmptyList()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolIdAndPaymentMade<AllOrdersViewModel>(1);

            Assert.That(result, Is.Empty);
        }

          [Test]
        public void GetOrdersBySchoolIdAndPaymentMadeAndTrainerId_NoOrdersInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Order>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolIdPaymentMadeAndTrainerId<AllOrdersViewModel>(1,1);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetOrdersBySchoolIdAndPaymentMadeAndTrainerId_OrdersInDb_ReturnsAListWithOrders()
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
                SchoolId = 1,
                Category = Category.A
            };
            var courseB = new Course()
            {
                Id = 2,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty2"
                    }
                },
                TrainerId = 2,
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.PaymentReceived
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.PaymentUpdated

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.Completed

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolIdPaymentMadeAndTrainerId<AllOrdersViewModel>(1,2).Count();

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void GetOrdersBySchoolIdAndPaymentMadeAndTrainerId_NoOrdersWithSchoolIdDb_ReturnsEmptyList()
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
                SchoolId = 1,
                Category = Category.A
            };
            var courseB = new Course()
            {
                Id = 2,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty2"
                    }
                },
                TrainerId = 2,
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.PaymentUpdated
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.PaymentReceived

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.PaymentUpdated

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.Completed

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolIdPaymentMadeAndTrainerId<AllOrdersViewModel>(3,1);

            Assert.That(result, Is.Empty);
        }

         [Test]
        public void GetOrdersBySchoolIdAndPaymentMadeAndTrainerId_NoOrdersWithPaymentMadeDb_ReturnsEmptyList()
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
                SchoolId = 1,
                Category = Category.A
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.AwaitingPayment

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.AwaitingPayment

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolIdPaymentMadeAndTrainerId<AllOrdersViewModel>(1,2);

            Assert.That(result, Is.Empty);
        }

         [Test]
        public void GetOrdersBySchoolIdAndPaymentMadeAndTrainerId_NoOrdersWithTrainerIdDb_ReturnsEmptyList()
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
                SchoolId = 1,
                Category = Category.A
            };
            var courseB = new Course()
            {
                Id = 2,
                Trainer = new Trainer()
                {
                    User = new AppUser()
                    {
                        Nickname = "Besty2"
                    }
                },
                TrainerId = 2,
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
                Category = Category.B
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

            var returnValue = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.PaymentUpdated
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Customer =customerB,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.PaymentReceived

                },
                new Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 2,
                    Course = courseB,
                    OrderStatus = OrderStatus.PaymentUpdated

                },
                new Order() {
                    Id = 4,
                    CustomerId = 1,
                    Customer =customerA,
                    CourseId = 1,
                    Course = courseA,
                    OrderStatus = OrderStatus.Completed

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.orderService.GetOrdersBySchoolIdPaymentMadeAndTrainerId<AllOrdersViewModel>(1,3);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Create_ValidInputModel_ReturnsCreatedOrder()
        {
            var order = new Order()
            {
                CourseId = 1,
                CustomerId = 2,
                ActualPriceWhenOrder = 200
            };

            this.repository.Setup(r => r.AddAsync(order)).Returns(Task.FromResult(order));

            var model = new CreateOrderInputModel()
            {
                CourseId = 1,
                CustomerId = 2,
                ActualPriceWhenOrder = 200
            };

            var result = this.orderService.Create(model).GetAwaiter().GetResult();

            Assert.That(result.CourseId, Is.EqualTo(1));
            Assert.That(result.CustomerId, Is.EqualTo(2));
            Assert.That(result,Is.TypeOf<Order>());
        }

        [Test]
        public void CancelOrder_WithValidObject_ReturnsOrder()
        {
            var order = new Order()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                ActualPriceWhenOrder = 200,
                OrderStatus = OrderStatus.Completed
            };

            var returnValue = new List<Order>()
            {
                order
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            this.repository.Setup(r => r.Update(order)).Callback(() => new Order()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                ActualPriceWhenOrder = 200,
                OrderStatus = OrderStatus.Cancelled
            });

            var result = this.orderService.CancelOrder(1).GetAwaiter().GetResult();

          
            Assert.That(result.OrderStatus, Is.EqualTo(OrderStatus.Cancelled));
            Assert.That(result,Is.TypeOf<Order>());
        }

        [Test]
        public void ChangeStatus_WithValidObject_ReturnsOrder()
        {
            var order = new Order()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                ActualPriceWhenOrder = 200,
                OrderStatus = OrderStatus.Completed
            };

            var returnValue = new List<Order>()
            {
                order
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);

            this.repository.Setup(r => r.Update(order)).Callback(() => new Order()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                ActualPriceWhenOrder = 200,
                OrderStatus = OrderStatus.Cancelled
            });

            var result = this.orderService.ChangeStatus(1,OrderStatus.Expired).GetAwaiter().GetResult();

          
            Assert.That(result.OrderStatus, Is.EqualTo(OrderStatus.Expired));
            Assert.That(result,Is.TypeOf<Order>());
        }

        [Test]
        public void Delete_WithValidId_ReturnsOrder()
        {
            var order = new Order()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                ActualPriceWhenOrder = 200,
                OrderStatus = OrderStatus.Completed
            };

            var returnValue = new List<Order>()
            {
                order
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable);
            this.repository.Setup(r => r.Delete(order)).Callback(() => new Order()
            {
                Id = 1,
                CourseId = 1,
                CustomerId = 2,
                ActualPriceWhenOrder = 200,
                OrderStatus = OrderStatus.Completed
            });

            var result = this.orderService.Delete(1,"a").GetAwaiter().GetResult();

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
