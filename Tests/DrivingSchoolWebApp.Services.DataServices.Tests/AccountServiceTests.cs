namespace DrivingSchoolWebApp.Services.DataServices.Tests
{
    using System;
    using Data.Common;
    using Data.Models;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Models.Account;
    using Models.Car;
    using Moq;
    using NUnit.Framework;
    using NUnit.Framework.Internal;

    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IRepository<AppUser>> repository;
        private AccountService accountService;
        private Mock<UserManager<AppUser>> userManager;
        private Mock<ILogger<RegisterViewModel>> logger;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository<AppUser>>();
            this.userManager = new Mock<UserManager<AppUser>>(
                Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
            this.logger = new Mock<ILogger<RegisterViewModel>>();
            //this.accountService = new AccountService(this.repository.Object,);
            this.SetMapper();
        }
        // return new Mock;

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
