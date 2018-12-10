namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Account;

    public interface IAccountService
    {
        IActionResult Register(RegisterViewModel user);

        IActionResult Login(LoginViewModel model);

        IActionResult Logout();

        AppUser GetUser(string username);

        void CreateUserExternal();

        //todo AuthenticationProperties ConfigureExternalLoginProperties(string provider, string redirectUrl);

        //todo void Demote(string id);

        //todo void Promote(string id);

        //todo IEnumerable<AdminPanelUsersViewModel> AdminPanelUsers();
    }
}
