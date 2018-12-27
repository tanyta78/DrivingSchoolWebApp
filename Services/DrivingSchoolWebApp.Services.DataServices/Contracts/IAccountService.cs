namespace DrivingSchoolWebApp.Services.DataServices.Contracts
{
    using System.Collections.Generic;
    using Data.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Models.Account;
    using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

    public interface IAccountService
    {
        IdentityResult Register(RegisterViewModel user);

        SignInResult Login(AppUser user, string password);

        void Logout();

        AppUser GetUser(string username);

        TViewModel GetUserById<TViewModel>(string id);

        AppUser GetUserById(string id);
        
        void CreateUserExternal();

        AuthenticationProperties ConfigureExternalLoginProperties(string provider, string redirectUrl);

        void UpdateUserProfile(EditUserProfileInputModel model);

        void Demote(string id);

        void Promote(string id);

        void Enable(string id);

        void Disable(string id);

        void Approve(string id);

        void SetRole(string role, string userId);

        void RemoveRole(string role, string userId);
        
        void RemoveApproval(string id);

        IEnumerable<AdminPanelUsersViewModel> AdminPanelUsers();

        IEnumerable<SchoolManageUsersViewModel> AllNonManager();

    }
}
