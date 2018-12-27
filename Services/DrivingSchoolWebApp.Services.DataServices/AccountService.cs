namespace DrivingSchoolWebApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using AutoMapper;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Models.Account;

    public class AccountService : IAccountService
    {
        private readonly IRepository<AppUser> userRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IMapper mapper;
        private readonly ISchoolService schoolService;
        private readonly ILogger<RegisterViewModel> logger;

        public AccountService(IRepository<AppUser> userRepository, UserManager<AppUser> userManager, ILogger<RegisterViewModel> logger, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IMapper mapper, ISchoolService schoolService)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.schoolService = schoolService;
            this.logger = logger;
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

            var result = this.userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();

            if (!result.Succeeded) return result;

            if (this.userRepository.All().Count() == 1)
            {
                this.userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
            }
            else
            {
                this.userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();

            }

            if (user.UserType != UserType.School)
            {
                user.IsApproved = true;
                this.userRepository.SaveChangesAsync().GetAwaiter().GetResult();

            }

            this.logger.LogInformation("User created a new account with password.");

            var code = this.userManager.GenerateEmailConfirmationTokenAsync(user).GetAwaiter().GetResult();

            return result;
        }

        public Microsoft.AspNetCore.Identity.SignInResult Login(AppUser user, string password)
        {
            return this.signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: true).GetAwaiter().GetResult();
        }

        public void Logout()
        {
            this.signInManager.SignOutAsync().GetAwaiter().GetResult();

            ////return this.Redirect("/");
        }

        public AppUser GetUser(string username)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.UserName == username);
            return user;
        }

        public TViewModel GetUserById<TViewModel>(string id)
        {
            var user = this.GetUserById(id);
            return this.mapper.Map<TViewModel>(user);
        }


        public AppUser GetUserById(string id)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == id);
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

        public AuthenticationProperties ConfigureExternalLoginProperties(string provider, string redirectUrl)
        {
            return this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public void UpdateUserProfile(EditUserProfileInputModel model)
        {
            var user = this.GetUserById(model.Id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Nickname = model.Nickname;
            user.BirthDate = model.BirthDate;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            this.userRepository.Update(user);
            this.userRepository.SaveChangesAsync().GetAwaiter().GetResult();

        }

        public void Demote(string id)
        {
            this.RemoveRole("Admin", id);
        }

        public void Promote(string id)
        {
            this.SetRole("Admin", id);
        }

        public void Approve(string id)
        {
            var user = this.GetUserById(id);
            //todo check for null
            if (user.UserType != UserType.School) return;
            this.SetRole("School", id);

            this.schoolService.ApproveSchool(user);
            user.IsApproved = true;
            
            this.userRepository.Update(user);
            this.userRepository.SaveChangesAsync().GetAwaiter().GetResult();

        }

        public void SetRole(string role, string userId)
        {
            var user = this.GetUserById(userId);
            this.userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();
        }

        public void RemoveRole(string role, string userId)
        {
            var user = this.GetUserById(userId);
            this.userManager.RemoveFromRoleAsync(user, role).GetAwaiter().GetResult();
        }

        public void RemoveApproval(string id)
        {
            var user = this.GetUserById(id);
            this.RemoveRole("School", id);
            //let  user.UserType  to school to be available to create by choosing from admin new school;
            user.IsApproved = false;
            this.userRepository.Update(user);
            this.userRepository.SaveChangesAsync().GetAwaiter().GetResult();

        }

        public void Enable(string id)
        {
            var user = this.GetUserById(id);
            user.IsEnabled = true;
            
            this.userRepository.Update(user);
            this.userRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void Disable(string id)
        {
            var user = this.GetUserById(id);
            user.IsEnabled = false;

            this.userRepository.Update(user);
            this.userRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<AdminPanelUsersViewModel> AdminPanelUsers()
        {
            var users = new List<AdminPanelUsersViewModel>();

            foreach (var userDb in this.userRepository.All().ToList())
            {
                var userc = this.signInManager.Context.User;

                if (userDb.UserName == userc.Identity.Name)
                {
                    continue;
                }
                var user = new AdminPanelUsersViewModel()
                {
                    Id = userDb.Id,
                    IsEnabled = userDb.IsEnabled,
                    Username = userDb.UserName,
                    UserType = userDb.UserType,
                    IsApproved = userDb.IsApproved
                };

                var rolesAsString = this.userManager.GetRolesAsync(userDb).Result;

                foreach (var role in rolesAsString)
                {
                    if (role != null) user.Role.Add(role);
                }

                users.Add(user);
            }

            return users;
        }

        public IEnumerable<SchoolManageUsersViewModel> AllNonManager()
        {
            var users = new List<SchoolManageUsersViewModel>();

            //get all users which has school type, but not approved 
            var usersNonManagerWithSchollType = this.userRepository.All()
                .Where(u => u.UserType == UserType.School && u.IsApproved == false && u.IsEnabled);

            foreach (var userDb in usersNonManagerWithSchollType)
            {

                var user = new SchoolManageUsersViewModel()
                {
                    Id = userDb.Id,
                    Username = userDb.UserName,
                };

                users.Add(user);
            }

            return users;
        }

    }
}
