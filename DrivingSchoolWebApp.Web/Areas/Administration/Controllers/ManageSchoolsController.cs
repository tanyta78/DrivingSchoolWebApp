namespace DrivingSchoolWebApp.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.School;
    using Web.Controllers;
    using X.PagedList;

    [Area("Administration")]
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
            var school = this.schoolService.GetSchoolById<SchoolViewModel>(id);

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

            var schoolId = this.schoolService.Create(model).Id;

            //set user role
            this.accountService.SetRole("School", model.ManagerId);
            
            //todo decide where to redirect
            return this.RedirectToAction("Details", "ManageSchools", schoolId);
        }

        // GET: Administration/ManageSchools/Edit/5
        public IActionResult Edit(int id)
        {
            var school = this.schoolService.GetSchoolById<EditSchoolInputModel>(id);
            return this.View(school);
        }

        // POST: Administration/ManageSchools/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public IActionResult Edit(EditSchoolInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var schoolId = this.schoolService.Edit(model).Id;

            //todo decide where to redirect
            return this.RedirectToAction("Details", "ManageSchools", schoolId);
        }

        // GET: Administration/ManageSchools/Delete/5
        public IActionResult Delete(int id)
        {
            var school = this.schoolService.GetSchoolById<DeleteSchoolViewModel>(id);
            return this.View(school);
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

        // GET: Administration/ManageSchools/ChangeManager/5
        public IActionResult ChangeManager(int id)
        {
            this.ViewBag.UsersIds = this.accountService.AllNonManager();
            var school = this.schoolService.GetSchoolById<ChangeManagerSchoolViewModel>(id);
            return this.View();
        }

        // POST: Administration/ManageSchools/ChangeManager/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public IActionResult ChangeManager(ChangeManagerSchoolViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var newManager = this.accountService.GetUser(model.ManagerId);
            this.schoolService.ChangeManager(model.Id, model.ManagerId);

            this.accountService.RemoveApproval(model.ManagerId);
            this.accountService.Approve(model.NewManagerId);

            return this.RedirectToAction(nameof(this.Index));
        }


    }
}