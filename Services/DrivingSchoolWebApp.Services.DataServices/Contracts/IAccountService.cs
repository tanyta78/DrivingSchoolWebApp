namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Models.Account;

    public interface IAccountService
    {
        IdentityResult Register(RegisterViewModel user);

        SignInResult Login(LoginViewModel model);

        void Logout();

        AppUser GetUser(string username);

        void CreateUserExternal();

        //todo AuthenticationProperties ConfigureExternalLoginProperties(string provider, string redirectUrl);

        //todo void Demote(string id);

        //todo void Promote(string id);

        //todo IEnumerable<AdminPanelUsersViewModel> AdminPanelUsers();
    }
}
