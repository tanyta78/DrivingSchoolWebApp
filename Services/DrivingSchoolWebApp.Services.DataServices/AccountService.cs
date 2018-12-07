namespace DrivingSchoolWebApp.Services.DataServices
{
    using System.Linq;
    using System.Security.Claims;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Models.Account;

    public class AccountService : IAccountService
    {
        private readonly IRepository<AppUser> userRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly ILogger<RegisterViewModel> logger;

        public AccountService(IRepository<AppUser> userRepository, UserManager<AppUser> userManager, ILogger<RegisterViewModel> logger, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }


        public AppUser GetUser(string username)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.UserName == username);
            return user;
        }

        public void CreateUserExternal()
        {
            var info = this.signInManager.GetExternalLoginInfoAsync().GetAwaiter().GetResult();
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var user = this.userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                user = new AppUser() { UserName = email, Email = email };
                var result = this.userManager.CreateAsync(user).Result;
            }

            this.signInManager.SignInAsync(user, false).GetAwaiter().GetResult();

        }

        //public AuthenticationProperties ConfigureExternalLoginProperties(string provider, string redirectUrl)
        //{
        //    return this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //}

        //public void Demote(string id)
        //{
        //    var user = this.userManager.Users.Where(u => u.Id == id).FirstOrDefault();
        //    this.userManager.RemoveFromRoleAsync(user, "Admin").GetAwaiter().GetResult();
        //}

        //public void Promote(string id)
        //{
        //    var user = this.userManager.Users.Where(u => u.Id == id).FirstOrDefault();
        //    this.userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
        //}

        //public IList<AdminPanelUsersViewModel> AdminPanelUsers()
        //{
        //    var users = new List<AdminPanelUsersViewModel>();
        //    foreach (var u in this.userManager.Users.ToList())
        //    {
        //        var user = new AdminPanelUsersViewModel
        //        {
        //            Username = u.UserName,
        //            Id = u.Id
        //        };
        //        var roleIds = this.db.UserRoles.Where(r => r.UserId == u.Id).ToList();

        //        foreach (var roleId in roleIds)
        //        {
        //            user.Role.Add(this.roleManager.Roles.Where(r => r.Id == roleId.RoleId).FirstOrDefault().Name);
        //        }

        //        users.Add(user);
        //    }
        //    return users;
        //}

        public SignInResult Login(LoginViewModel model)
        {
            var user = this.GetUser(model.Username);
            var result = this.signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true).Result;
            return result;
        }

        public async void Logout()
        {
            await this.signInManager.SignOutAsync();
        }

        public IdentityResult Register(RegisterViewModel model)
        {

            var user = new AppUser()
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Nickname = model.Nickname,
                BirthDate = model.BirthDate,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                UserType = model.UserType
            };

            var result = this.userManager.CreateAsync(user, model.Password).Result;

            return result;
        }
    }
}
