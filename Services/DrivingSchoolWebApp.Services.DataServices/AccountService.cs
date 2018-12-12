namespace DrivingSchoolWebApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using Models.Account;

    public class AccountService : PageModel, IAccountService
    {
        private readonly IRepository<AppUser> userRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IMapper mapper;
        private readonly ISchoolService schoolService;
        private readonly ILogger<RegisterViewModel> logger;

        public AccountService(IRepository<AppUser> userRepository, UserManager<AppUser> userManager, ILogger<RegisterViewModel> logger, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IMapper mapper,ISchoolService schoolService)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.schoolService = schoolService;
            this.logger = logger;
        }


        public AppUser GetUser(string username)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.UserName == username);
            return user;
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

        public void Demote(string id)
        {
            var user = this.GetUserById(id);
            this.userManager.RemoveFromRoleAsync(user, "Admin").GetAwaiter().GetResult();
        }

        public void Promote(string id)
        {
            var user = this.GetUserById(id);
            this.userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
        }

        public void Approve(string id)
        {
            var user = this.GetUserById(id);
            //todo check for null
            if (user.UserType != UserType.School) return;
            this.userManager.AddToRoleAsync(user, "School").GetAwaiter().GetResult();
           
            this.schoolService.Create(user);
            user.IsApproved = true;
            this.userRepository.SaveChangesAsync().GetAwaiter().GetResult();

        }

        public void RemoveApproval(string id)
        {
            var user = this.GetUserById(id);
            //todo check for null
            this.userManager.RemoveFromRoleAsync(user, "School").GetAwaiter().GetResult();
            user.UserType = UserType.Customer;
            user.IsApproved = false;
            this.userRepository.SaveChangesAsync().GetAwaiter().GetResult();

        }

        public void Enable(string id)
        {
            var user = this.GetUserById(id);
            user.IsEnabled = true;
            this.userRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void Disable(string id)
        {
            var user = this.GetUserById(id);
            user.IsEnabled = false;
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

        public IActionResult Login(LoginViewModel model)
        {
            return this.LoginPostAsync(model).Result;
        }

        private async Task<IActionResult> LoginPostAsync(LoginViewModel model)
        {
            if (!this.ModelState.IsValid) return this.Page();

            var user = this.GetUser(model.Username);

            if (user == null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return this.Page();
            }

            if (!user.IsEnabled)
            {
                this.logger.LogWarning("User account locked out.");
                return this.Redirect("/Account/Lockout");
            }

            var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true);


            if (result.Succeeded)
            {
                this.logger.LogInformation("User logged in.");
                return this.Redirect("/");
            }

            if (result.IsLockedOut)
            {
                this.logger.LogWarning("User account locked out.");
                return this.RedirectToPage("Lockout");
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return this.Page();
            }
        }

        public IActionResult Logout()
        {

            return this.LogoutGetAsync().Result;
        }

        private async Task<IActionResult> LogoutGetAsync()
        {
            await this.signInManager.SignOutAsync();

            return this.Redirect("/");
        }

        public IActionResult Register(RegisterViewModel user)
        {
            return this.RegisterPostAsync(user).Result;
        }

        private async Task<IActionResult> RegisterPostAsync(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid) return this.Page();
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

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (this.userRepository.All().Count() == 1)
                {
                    await this.userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, "User");

                    if (user.UserType == UserType.Customer)
                    {
                        user.IsApproved = true;
                        //todo add create customer
                    }
                    if (user.UserType == UserType.Trainer)
                    {
                        user.IsApproved = true;

                        //todo add create trainer
                        //not login after creation redirect to list or detail view of trainer
                        return this.Redirect("/Trainers/All");
                    }

                }

                this.logger.LogInformation("User created a new account with password.");

                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                await this.signInManager.SignInAsync(user, isPersistent: false);

                return this.Redirect("/");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.Page();
        }
    }
}
