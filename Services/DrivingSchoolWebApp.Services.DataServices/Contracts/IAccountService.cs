namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Models.Account;

    public interface IAccountService
    {
        IActionResult Register(RegisterViewModel user);

        IActionResult Login(LoginViewModel model);

        IActionResult Logout();

        AppUser GetUser(string username);

        void CreateUserExternal();

        AuthenticationProperties ConfigureExternalLoginProperties(string provider, string redirectUrl);

        void Demote(string id);

        void Promote(string id);

        void Enable(string id);

        void Disable(string id);

        void Approve(string id);

        void RemoveApproval(string id);
        
        IEnumerable<AdminPanelUsersViewModel> AdminPanelUsers();
    }
}
