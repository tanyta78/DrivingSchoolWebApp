namespace DrivingSchoolWebApp.Web.Controllers
{
    using Microsoft.AspNetCore.Http;
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

        // GET: Schools
        public ActionResult Index()
        {
            var schoolsApproved = this.schoolService.AllActiveSchools<SchoolViewModel>();
            var model = new SchoolListViewModel()
            {
                ActiveSchools = schoolsApproved
            };
            return this.View(model);
        }

        // GET: Schools/Details/5
        public ActionResult Details(int id)
        {
            var school = this.schoolService.GetSchoolById<SchoolViewModel>(id);

            //todo check for null

            return this.View(school);
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Schools/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: Schools/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Schools/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: Schools/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Schools/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}