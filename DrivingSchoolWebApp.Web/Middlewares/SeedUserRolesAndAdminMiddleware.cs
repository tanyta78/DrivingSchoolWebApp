namespace DrivingSchoolWebApp.Web.Middlewares
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore.Internal;

    public class SeedUserRolesAndAdminMiddleware
    {
        private readonly RequestDelegate next;

        public SeedUserRolesAndAdminMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, AppDbContext dbContext,IServiceProvider serviceProvider)
        {
            if (!dbContext.Users.Any())
            {
                AppDbContextSeeder.Seed(dbContext, serviceProvider);
            }
           
            await this.next(httpContext);
        }
    }
}
