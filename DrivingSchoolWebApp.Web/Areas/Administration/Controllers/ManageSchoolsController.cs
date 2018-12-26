namespace DrivingSchoolWebApp.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.School;
    using Web.Controllers;
    using X.PagedList;

    public class ManageSchoolsController : BaseController
    {
        private readonly ISchoolService schoolService;
        private readonly IAccountService accountService;

        public ManageSchoolsController(ISchoolService schoolService, IAccountService accountService)
        {
            this.schoolService = schoolService;
            this.accountService = accountService;
        }

        // GET: Administration/ManageSchools
        public IActionResult Index(int? page)
        {
            var schoolsApproved = this.schoolService.AllActiveSchools<SchoolViewModel>();

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            IPagedList<SchoolViewModel> model = schoolsApproved.ToPagedList(pageNumber, 5); // will only contain 5 products max because of the pageSize

            return this.View(model);
        }


        // GET: Administration/ManageSchools/Details/5
        public IActionResult Details(int id)
        {
            var school = this.schoolService.GetSchoolById(id);
            //todo map view model

            return this.View(school);
        }

        // GET: Administration/ManageSchools/Create
        public IActionResult Create()
        {
            this.ViewBag.UsersIds = this.accountService.AllNonManager();
            return this.View();
        }

        // POST: Administration/ManageSchools/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSchoolInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var school = this.schoolService.Create(model);

            //set user role
            this.accountService.SetRole("School", model.ManagerId);

            //todo decide where to redirect
            return this.RedirectToAction("Details", "Schools", school.Id);
        }

        // GET: Administration/ManageSchools/Edit/5
        public IActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Administration/ManageSchools/Edit/5
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

        // GET: Administration/ManageSchools/Delete/5
        public IActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Administration/ManageSchools/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public IActionResult Delete(int id, string username)
        {
            this.schoolService.Delete(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}