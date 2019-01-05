namespace DrivingSchoolWebApp.Web.Areas.SchoolManage.Controllers
{
    using System;
    using System.Security.Claims;
    using Data.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.School;
    using Web.Controllers;

    [Area(GlobalDataConstants.SchoolArea)]
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
        [Authorize(Roles = GlobalDataConstants.SchoolRoleName)]
        public IActionResult Manage()
        {
            var username = this.User.Identity.Name;
            var school = this.schoolService.GetSchoolByManagerName<SchoolViewModel>(username);

            return this.View(school);
        }

        // GET: SchoolManage/Schools/Create
        [Authorize(Roles = GlobalDataConstants.SchoolRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Schools/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalDataConstants.SchoolRoleName)]
        public IActionResult Create(CreateSchoolInputModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                var schoolId = this.schoolService.Create(model).Id;
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
        [Authorize(Roles = GlobalDataConstants.SchoolRoleName)]
        public IActionResult Edit(int id)
        {
            var school = this.schoolService.GetSchoolById<EditSchoolInputModel>(id);
            return this.View(school);
        }

        // POST: Schools/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalDataConstants.SchoolRoleName)]
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
                    throw new OperationCanceledException(GlobalDataConstants.NoRights);
                }

                var schoolId = this.schoolService.Edit(model).Id;

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

            var result = (userId == school.ManagerId) || this.User.IsInRole("Admin");
            return result;
        }

    }
}