namespace DrivingSchoolWebApp.Web.Controllers
{
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Account;

    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;
        private readonly ITrainerService trainerService;
        private readonly ICustomerService customerService;
        private readonly ISchoolService schoolService;

        public AccountController(IAccountService accountService, ITrainerService trainerService, ICustomerService customerService, ISchoolService schoolService)
        {
            this.accountService = accountService;
            this.trainerService = trainerService;
            this.customerService = customerService;
            this.schoolService = schoolService;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return this.View();
        }

        // POST: Account/Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid) return this.View();

            var user = this.accountService.GetUser(model.Username);

            if (user == null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return this.View();
            }

            if (!user.IsEnabled)
            {
                // this.logger.LogWarning("User account locked out.");
                return this.Redirect("/Account/Lockout");
            }

            var result = this.accountService.Login(user, model.Password);
            if (result.Succeeded)
            {
                // this.logger.LogInformation("User logged in.");
                //todo redirect ot different area depending on user role - user, school,admin
                return this.Redirect("/");
            }

            if (result.IsLockedOut)
            {
                //  this.logger.LogWarning("User account locked out.");
                return this.RedirectToPage("Lockout");
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return this.View();
            }

        }

        //POST:Account/ExternalLogin
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = "/Account/ExternalLogin";
            AuthenticationProperties properties = this.accountService.ConfigureExternalLoginProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }


        public IActionResult ExternalLogin()
        {
            this.accountService.CreateUserExternal();
            return this.RedirectToAction("Index", "Home");
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return this.View();
        }

        // POST: Account/Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid) return this.View();

            var result = this.accountService.Register(model);

            if (result.Succeeded)
            {
                var user = this.accountService.GetUser(model.Username);

                if (model.UserType == UserType.Customer)
                {
                    this.customerService.Create(user);

                }

                if (model.UserType == UserType.Trainer)
                {
                    var managerName = this.User.Identity.Name;
                    var school = this.schoolService.GetSchoolByManagerName(managerName);

                    //todo check fo null===
                    this.trainerService.Hire(user.Id, school.Id);
                    //todo add schoolid if is needed in path
                    return this.RedirectToAction("All", "Trainers");
                }

                return this.RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.View();
        }

        public IActionResult Logout()
        {
            this.accountService.Logout();
            return this.Redirect("/");
        }

        public IActionResult Lockout()
        {
            return this.View();
        }


    }
}