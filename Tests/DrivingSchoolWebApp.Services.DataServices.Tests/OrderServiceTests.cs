namespace DrivingSchoolWebApp.Services.DataServices.Tests
{
    using System;
    using Data.Common;
    using Data.Models;
    using Mapping;
    using Models.Account;
    using Models.Car;
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
