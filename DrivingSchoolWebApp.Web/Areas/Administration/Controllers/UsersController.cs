namespace DrivingSchoolWebApp.Web.Areas.Administration.Controllers
{
    using Data.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Account;
    using Web.Controllers;

    [Area(GlobalDataConstants.AdministratorArea)]
    public class UsersController : BaseController
    {
        private readonly IAccountService accountService;

        public UsersController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        // GET: Administration/Users/AdminPanel
        [Authorize(Roles = GlobalDataConstants.AdministratorRoleName)]
        public IActionResult AdminPanel()
        {
            var users = new AdminPanelUsersListViewModel { Users = this.accountService.AdminPanelUsers() };
            return this.View(users);
        }

        // GET: Administration/Users/Demote/id
        [Authorize(Roles = GlobalDataConstants.AdministratorRoleName)]
        public IActionResult Demote(string id)
        {
            this.accountService.Demote(id);
            return this.RedirectToAction("AdminPanel");
        }

        // GET: Administration/Users/Promote/id
        [Authorize(Roles = GlobalDataConstants.AdministratorRoleName)]
        public IActionResult Promote(string id)
        {
            this.accountService.Promote(id);
            return this.RedirectToAction("AdminPanel");
        }

        // GET: Administration/Users/Approve/id
        [Authorize(Roles = GlobalDataConstants.AdministratorRoleName)]
        public IActionResult Approve(string id)
        {
            this.accountService.Approve(id);
            return this.RedirectToAction("AdminPanel");
        }

        // GET: Administration/Users/RemoveApproval/id
        [Authorize(Roles = GlobalDataConstants.AdministratorRoleName)]
        public IActionResult RemoveApproval(string id)
        {
            this.accountService.RemoveApproval(id);
            return this.RedirectToAction("AdminPanel");
        }

        // GET: Administration/Users/Enable/id
        [Authorize(Roles = GlobalDataConstants.AdministratorRoleName)]
        public IActionResult Enable(string id)
        {
            this.accountService.Enable(id);
            return this.RedirectToAction("AdminPanel");
        }

        // GET: Administration/Users/Disable/id
        [Authorize(Roles = GlobalDataConstants.AdministratorRoleName)]
        public IActionResult Disable(string id)
        {
            this.accountService.Disable(id);
            return this.RedirectToAction("AdminPanel");
        }

    }
}