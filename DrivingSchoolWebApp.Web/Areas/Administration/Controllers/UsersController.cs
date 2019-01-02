namespace DrivingSchoolWebApp.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Account;
    using Web.Controllers;

    [Area("Administration")]
    public class UsersController : BaseController
    {
        private readonly IAccountService accountService;

        public UsersController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        // GET: Administration/Users/AdminPanel
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            var users = new AdminPanelUsersListViewModel { Users = this.accountService.AdminPanelUsers() };
            return this.View(users);
        }

        // GET: Administration/Users/Demote/id
        [Authorize(Roles = "Admin")]
        public IActionResult Demote(string id)
        {
            this.accountService.Demote(id);
            return this.RedirectToAction("AdminPanel");
        }

        // GET: Administration/Users/Promote/id
        [Authorize(Roles = "Admin")]
        public IActionResult Promote(string id)
        {
            this.accountService.Promote(id);
            return this.RedirectToAction("AdminPanel");
        }

        // GET: Administration/Users/Approve/id
        [Authorize(Roles = "Admin")]
        public IActionResult Approve(string id)
        {
            this.accountService.Approve(id);
            return this.RedirectToAction("AdminPanel");
        }

        // GET: Administration/Users/Enable/id
        [Authorize(Roles = "Admin")]
        public IActionResult Enable(string id)
        {
            this.accountService.Enable(id);
            return this.RedirectToAction("AdminPanel");
        }

        // GET: Administration/Users/Disable/id
        [Authorize(Roles = "Admin")]
        public IActionResult Disable(string id)
        {
            this.accountService.Disable(id);
            return this.RedirectToAction("AdminPanel");
        }

    }
}