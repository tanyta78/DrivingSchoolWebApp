namespace DrivingSchoolWebApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Index()
        {
            var schoolsApproved = this.schoolService.AllActiveSchools<SchoolViewModel>();
            var model = new SchoolListViewModel()
            {
                ActiveSchools = schoolsApproved
            };
            return this.View(model);
        }

        // GET: Schools/Manage
        public IActionResult Manage()
        {
            var username = this.User.Identity.Name;
            var school = this.schoolService.GetSchoolByManagerName<SchoolViewModel>(username);
            
            return this.View(school);
        }

        // GET: Schools/Details/5
        public IActionResult Details(int id)
        {
            var school = this.schoolService.GetSchoolById<SchoolViewModel>(id);
            return this.View(school);
        }

        // GET: Schools/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Schools/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSchoolInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var school = this.schoolService.Create(model.Manager);

            //todo decide where to redirect
            return this.RedirectToAction("Details", "Schools", school.Id);
        }

        // GET: Schools/Edit/5
        public IActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Schools/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditSchoolInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var school = this.schoolService.Edit(model);

            //todo decide where to redirect
            return this.RedirectToAction("Details", "Schools", school.Id);
        }

        // GET: Schools/Delete/5
        public IActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Schools/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public IActionResult Delete(int id, string username)
        {
            this.schoolService.Delete(id);
            return this.RedirectToAction(nameof(Index));
        }
    }
}