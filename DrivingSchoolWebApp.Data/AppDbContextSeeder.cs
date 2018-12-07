namespace DrivingSchoolWebApp.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Models.Enums;

    public class AppDbContextSeeder
   {
    public static void Seed(AppDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            Seed(dbContext, roleManager, userManager);
        }

        public static void Seed(AppDbContext dbContext, RoleManager<AppRole> roleManager,
            UserManager<AppUser> userManager)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }

            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }

            SeedRoles(roleManager);
            SeedAdminUser(userManager).GetAwaiter().GetResult();
        }

        private static async Task SeedAdminUser(UserManager<AppUser> userManager)
        {
            var users = userManager.Users.ToList();

            AppUser admin = await userManager.FindByNameAsync("Admin");

            if (admin == null)
            {
                admin = new AppUser()
                {
                    UserName = "Admin",
                    Email = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "Adminov",
                    Nickname = "Adm",
                    BirthDate = DateTime.Now-TimeSpan.FromDays(365*18),
                    Address = "Lavender",
                    PhoneNumber = "555-5555-5555",
                    UserType = UserType.Undefined
                };
                await userManager.CreateAsync(admin, "123456");
                await userManager.AddToRoleAsync(admin, "Admin");
                
            }
        }

        private static void SeedRoles(RoleManager<AppRole> roleManager)
        {
            foreach (var roleName in GlobalDataConstants.RolesName)
            {
                SeedRole(roleName, roleManager);
            }
        }

        private static void SeedRole(string roleName, RoleManager<AppRole> roleManager)
        {
            var role = roleManager.FindByNameAsync(roleName).GetAwaiter().GetResult();
            if (role == null)
            {
                var result = roleManager.CreateAsync(new AppRole(roleName)).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
