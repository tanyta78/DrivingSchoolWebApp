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
    using Models.Payment;
    using Moq;
    using NUnit.Framework;
    using NUnit.Framework.Internal;

    [TestFixture]
    public class PaymentServiceTests
    {
        private Mock<IRepository<Payment>> repository;
        private PaymentService paymentService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<Payment>>();
            this.paymentService = new PaymentService(this.repository.Object);
            this.SetMapper();
        }

         [Test]
        public void All_NoPaymentsInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Payment>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.paymentService.All();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void All_PaymentsInDb_ReturnsAListWithPayments()
        {
            var returnValue = new List<Payment>()
            {
                new Payment(),
                new Payment(),
                new Payment()
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.paymentService.All().Count();

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void GenericAll_NoPaymentsInDb_ReturnsAnEmptyList()
        {
            var returnValue = new List<Payment>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.paymentService.All<AllPaymentsViewModel>();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GenericAll_PaymentsInDb_ReturnsAListWithPayments()
        {
          
            var returnValue = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                   OrderId = 1,
                   Amount = 200,
                   PaymentMethod = PaymentMethod.Cash
                },
                new Payment()
                {
                    Id = 2,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment()
                {
                    Id = 3,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment() {
                    Id = 4,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                }
            };

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.paymentService.All<AllPaymentsViewModel>().Count();

            Assert.That(result, Is.EqualTo(4));
        }

         [Test]
        public void GenericGetPaymentById_PaymentsInDb_ReturnsPaymentWithId()
        {
            var returnValue = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash
                },
                new Payment()
                {
                    Id = 2,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment()
                {
                    Id = 3,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment() {
                    Id = 4,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.paymentService.GetPaymentById<AllPaymentsViewModel>(2);

            Assert.That(result.Id, Is.EqualTo(2));
            Assert.That(result.OrderId, Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<AllPaymentsViewModel>());
        }

        [Test]
        public void GenericGetPaymentById_NoPaymentWithIdInDb_ThrowsException()
        {
            var returnValue = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash
                },
                new Payment()
                {
                    Id = 2,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment()
                {
                    Id = 3,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment() {
                    Id = 4,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                }
            };
          
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.paymentService.GetPaymentById<AllPaymentsViewModel>(5), Throws.ArgumentException);
        }

        [Test]
        public void GenericGetPaymentById_NoPaymentsInDb_ThrowsException()
        {
            var returnValue = new List<Payment>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            Assert.That(() => this.paymentService.GetPaymentById<AllPaymentsViewModel>(5), Throws.ArgumentException);
        }

          [Test]
        public void GenericGetPaymentByOrderId_PaymentsInDb_ReturnsPaymentsWithOrderId()
        {
            var returnValue = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash
                },
                new Payment()
                {
                    Id = 2,
                    OrderId = 2,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment()
                {
                    Id = 3,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment() {
                    Id = 4,
                    OrderId = 3,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.paymentService.GetPaymentsByOrderId<AllPaymentsViewModel>(1).ToList();

            Assert.That(result.Count(), Is.EqualTo(2));
           
            Assert.That(result, Is.TypeOf<List<AllPaymentsViewModel>>());
        }

        [Test]
        public void GenericGetPaymentByOrderId_NoPaymentWithOrderIdInDb_ReturnsEmptyList()
        {
            var returnValue = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash
                },
                new Payment()
                {
                    Id = 2,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment()
                {
                    Id = 3,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment() {
                    Id = 4,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                }
            };
          
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());
            var result = this.paymentService.GetPaymentsByOrderId<AllPaymentsViewModel>(2);
            Assert.That(result,Is.Empty);
        }

        [Test]
        public void GenericGetPaymentByOrderId_NoPaymentsInDb_TReturnsEmptyList()
        {
            var returnValue = new List<Payment>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());
            var result = this.paymentService.GetPaymentsByOrderId<AllPaymentsViewModel>(2);
            Assert.That(result,Is.Empty);
        }

          [Test]
        public void GenericGetPaymentByMethod_PaymentsInDb_ReturnsPaymentsWithMethod()
        {
            var returnValue = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash
                },
                new Payment()
                {
                    Id = 2,
                    OrderId = 2,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.CreditCard

                },
                new Payment()
                {
                    Id = 3,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.DebitCard

                },
                new Payment() {
                    Id = 4,
                    OrderId = 3,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.paymentService.GetPaymentsByMethod<AllPaymentsViewModel>(PaymentMethod.Cash).ToList();

            Assert.That(result.Count(), Is.EqualTo(2));
           
            Assert.That(result, Is.TypeOf<List<AllPaymentsViewModel>>());
        }

        [Test]
        public void GenericGetPaymentByMethod_NoPaymentWithMethodIdInDb_ReturnsEmptyList()
        {
            var returnValue = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash
                },
                new Payment()
                {
                    Id = 2,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.CreditCard

                },
                new Payment()
                {
                    Id = 3,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.CreditCard

                },
                new Payment() {
                    Id = 4,
                    OrderId = 1,
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                }
            };
          
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());
            var result = this.paymentService.GetPaymentsByMethod<AllPaymentsViewModel>(PaymentMethod.DebitCard);
            Assert.That(result,Is.Empty);
        }

        [Test]
        public void GenericGetPaymentByMethod_NoPaymentsInDb_ReturnsEmptyList()
        {
            var returnValue = new List<Payment>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());
            var result = this.paymentService.GetPaymentsByMethod<AllPaymentsViewModel>(PaymentMethod.Cash);
            Assert.That(result,Is.Empty);
        }

          [Test]
        public void GenericGetPaymentByCustomerId_PaymentsInDb_ReturnsPaymentsWithCustomerId()
        {
            var returnValue = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash
                },
                new Payment()
                {
                    Id = 2,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment()
                {
                    Id = 3,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment() {
                    Id = 4,
                    OrderId = 3,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 2
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.paymentService.GetPaymentsByCustomerId<AllPaymentsViewModel>(1).ToList();

            Assert.That(result.Count(), Is.EqualTo(3));
           
            Assert.That(result, Is.TypeOf<List<AllPaymentsViewModel>>());
        }

        [Test]
        public void GenericGetPaymentByCustomerId_NoPaymentWithCustomerIdInDb_ReturnsEmptyList()
        {
              var returnValue = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash
                },
                new Payment()
                {
                    Id = 2,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment()
                {
                    Id = 3,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment() {
                    Id = 4,
                    OrderId = 3,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 2
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                }
            };
          
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());
            var result = this.paymentService.GetPaymentsByCustomerId<AllPaymentsViewModel>(5);
            Assert.That(result,Is.Empty);
        }

        [Test]
        public void GenericGetPaymentByCustomerId_NoPaymentsInDb_TReturnsEmptyList()
        {
            var returnValue = new List<Payment>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());
            var result = this.paymentService.GetPaymentsByCustomerId<AllPaymentsViewModel>(2);
            Assert.That(result,Is.Empty);
        }

           [Test]
        public void GenericGetPaymentBySchoolId_PaymentsInDb_ReturnsPaymentsWithSchoolId()
        {
            var returnValue = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash
                },
                new Payment()
                {
                    Id = 2,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment()
                {
                    Id = 3,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment() {
                    Id = 4,
                    OrderId = 3,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 2
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                }
            };
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            var result = this.paymentService.GetPaymentsBySchoolId<AllPaymentsViewModel>(1).ToList();

            Assert.That(result.Count(), Is.EqualTo(3));
           
            Assert.That(result, Is.TypeOf<List<AllPaymentsViewModel>>());
        }

        [Test]
        public void GenericGetPaymentBySchoolId_NoPaymentWithSchoolIdInDb_ReturnsEmptyList()
        {
              var returnValue = new List<Payment>()
            {
                new Payment()
                {
                    Id = 1,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash
                },
                new Payment()
                {
                    Id = 2,
                    OrderId = 2,
                    Order = new Order()
                    {
                        CustomerId = 2,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment()
                {
                    Id = 3,
                    OrderId = 1,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 1
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                },
                new Payment() {
                    Id = 4,
                    OrderId = 3,
                    Order = new Order()
                    {
                        CustomerId = 1,
                        Course = new Course()
                        {
                            SchoolId = 2
                        }
                    },
                    Amount = 200,
                    PaymentMethod = PaymentMethod.Cash

                }
            };
          
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());
            var result = this.paymentService.GetPaymentsBySchoolId<AllPaymentsViewModel>(5);
            Assert.That(result,Is.Empty);
        }

        [Test]
        public void GenericGetPaymentBySchoolId_NoPaymentsInDb_TReturnsEmptyList()
        {
            var returnValue = new List<Payment>();
            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());

            this.repository.Setup(r => r.All()).Returns(returnValue.AsQueryable());
            var result = this.paymentService.GetPaymentsBySchoolId<AllPaymentsViewModel>(2);
            Assert.That(result,Is.Empty);
        }

        [Test]
        public void Create_ValidInputModel_ReturnsCreatedPayment()
        {
            var payment = new Payment()
            {
                OrderId = 1,
                PaymentMethod = PaymentMethod.Cash,
                Amount = 200
            };

            this.repository.Setup(r => r.AddAsync(payment)).Returns(Task.FromResult(payment));

            var model = new CreatePaymentInputModel()
            {
                OrderId = 1,
                PaymentMethod = PaymentMethod.Cash,
                Amount = 200
            };

            var result = this.paymentService.Create(model).GetAwaiter().GetResult();

            Assert.That(result.OrderId, Is.EqualTo(1));
            Assert.That(result.PaymentMethod, Is.EqualTo(PaymentMethod.Cash));
            Assert.That(result,Is.TypeOf<Payment>());
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
