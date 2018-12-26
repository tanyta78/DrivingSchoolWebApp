namespace DrivingSchoolWebApp.Web.Areas.SchoolManage.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.School;
    using Web.Controllers;

    public class SchoolsController : BaseController
    {
        private readonly ISchoolService schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            this.schoolService = schoolService;
        }

      // GET: Schools/Manage
        public IActionResult Manage()
        {
            var username = this.User.Identity.Name;
            var school = this.schoolService.GetSchoolByManagerName<EditSchoolInputModel>(username);
           //todo map view model
            
            return this.View(school);
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

            var schoolId = this.schoolService.Edit(model);

            //todo decide where to redirect
            return this.RedirectToAction("Manage", "Schools", schoolId);
        }

        
    }
}