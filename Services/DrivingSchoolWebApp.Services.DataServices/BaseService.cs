namespace DrivingSchoolWebApp.Services.DataServices
{
    using AutoMapper;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;

    public abstract class BaseService
    {
        protected BaseService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.Mapper = mapper;
        }

        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public IMapper Mapper { get; }
    }
}
