namespace DrivingSchoolWebApp.Web.Areas.SchoolManage.Controllers
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.School;
    using Web.Controllers;

    [Area("SchoolManage")]
    public class SchoolsController : BaseController
    {
        private readonly ISchoolService schoolService;
        private readonly IAccountService accountService;

        public SchoolsController(ISchoolService schoolService, IAccountService accountService)
        {
            this.schoolService = schoolService;
            this.accountService = accountService;
        }

        // GET: SchoolManage/Schools/Manage
        public IActionResult Manage()
        {
            var username = this.User.Identity.Name;
            var school = this.schoolService.GetSchoolByManagerName<SchoolViewModel>(username);

            return this.View(school);
        }

        // GET: SchoolManage/Schools/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Schools/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSchoolInputModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                var schoolId = this.schoolService.Create(model);
                this.accountService.SetRole("School", model.ManagerId);
                //todo decide where to redirect
                return this.RedirectToAction("Manage", "Schools", new { Area = "SchoolManage" });

            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // GET: Schools/Edit/5
        public IActionResult Edit(int id)
        {
            var school = this.schoolService.GetSchoolById<EditSchoolInputModel>(id);
            return this.View(school);
        }

        // POST: Schools/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditSchoolInputModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                if (!this.HasRights(model.Id))
                {
                    throw new OperationCanceledException("You do not have rights for this operation!");
                }

                var schoolId = this.schoolService.Edit(model);

                //todo decide where to redirect
                return this.RedirectToAction("Manage", "Schools", schoolId);

            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        private bool HasRights(int schoolId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var school = this.schoolService.GetSchoolById<DetailsSchoolViewModel>(schoolId);

            var result = (userId == school.ManagerUserId) || this.User.IsInRole("Admin");
            return result;
        }

    }
}