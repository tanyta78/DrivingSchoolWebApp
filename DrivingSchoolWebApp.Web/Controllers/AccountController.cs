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


        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
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
                //todo redirect to different area depending on user role - user, school,admin
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
                    var loginResult = this.accountService.Login(user, model.Password);
                    if (loginResult.Succeeded)
                    {
                        return this.RedirectToAction("Create", "Customers", new { Area = "", userId = user.Id });
                    }

                }

                if (model.UserType == UserType.Trainer)
                {
                    //var managerName = this.User.Identity.Name;
                    //var school = this.schoolService.GetSchoolByManagerName<EditSchoolInputModel>(managerName);

                    ////todo check fo null===
                    //this.trainerService.Hire(user.Id, school.Id);
                    ////todo add schoolid if is needed in path
                    return this.RedirectToAction("Create", "Trainers", new { Area = "SchoolManage", userId = user.Id });
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

        //todo Protect thie route for admins and schools changing his trainers user profiles
        // GET: Account/UpdateUserProfile/5
        public IActionResult UpdateUserProfile(string userId)
        {
            var userModel = this.accountService.GetUserById<EditUserProfileInputModel>(userId);

            return this.View(userModel);
        }

        // POST: Account/UpdateUserProfile/5
        [HttpPost]
        public IActionResult UpdateUserProfile(EditUserProfileInputModel model)
        {
            if (!this.ModelState.IsValid) return this.View(model);

            this.accountService.UpdateUserProfile(model);

            return this.RedirectToAction("Index", "Home");

        }

    }
}