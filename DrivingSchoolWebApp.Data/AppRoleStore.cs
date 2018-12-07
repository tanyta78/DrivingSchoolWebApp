namespace DrivingSchoolWebApp.Data
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Models;

    public class AppRoleStore :
       RoleStore<
           AppRole,
           AppDbContext,
           string,
           IdentityUserRole<string>,
           IdentityRoleClaim<string>>
    {
        public AppRoleStore(AppDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        protected override IdentityRoleClaim<string> CreateRoleClaim(AppRole role, Claim claim) =>
            new IdentityRoleClaim<string>
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            };
    }
}
