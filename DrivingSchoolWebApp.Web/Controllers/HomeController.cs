namespace DrivingSchoolWebApp.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.DataServices.Contracts;
    using Services.Models.School;

    public class HomeController : BaseController
    {
        private readonly ISchoolService schoolService;

        public HomeController(ISchoolService schoolService)
        {
            this.schoolService = schoolService;
        }

        public IActionResult Index()
        {
           return this.View();
        }

       public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }


    }
}

