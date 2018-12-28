
namespace DrivingSchoolWebApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.School;

    public class SchoolsController : BaseController
    {
        private readonly ISchoolService schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            this.schoolService = schoolService;
        }

        // GET: Schools/All
        public ActionResult All()
        {
            var schools = this.schoolService.AllActiveSchools<AllSchoolViewModel>();
            return this.View(schools);
        }

        // GET: Schools/Details/5
        public ActionResult Details(int id)
        {
            var school = this.schoolService.GetSchoolById<DetailsSchoolViewModel>(id);
            return this.View(school);
        }


    }
}