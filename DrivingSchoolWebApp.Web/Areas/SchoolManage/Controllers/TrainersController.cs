namespace DrivingSchoolWebApp.Web.Areas.SchoolManage.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.School;
    using Services.Models.Trainer;
    using Web.Controllers;

    public class TrainersController : BaseController
    {
        private readonly ITrainerService trainerService;
        private readonly ISchoolService schoolService;

        public TrainersController(ITrainerService trainerService, ISchoolService schoolService)
        {
            this.trainerService = trainerService;
            this.schoolService = schoolService;
        }

        // GET: Trainers/All
        public ActionResult All()
        {
            var managerName = this.User.Identity.Name;
            var school = this.schoolService.GetSchoolByManagerName<EditSchoolInputModel>(managerName);
            var trainers = this.trainerService.TrainersBySchoolId<TrainersViewModel>(school.Id);
            return this.View(trainers);
        }

        // GET: Trainers/Available
        public ActionResult Available()
        {
            var managerName = this.User.Identity.Name;
            var school = this.schoolService.GetSchoolByManagerName<EditSchoolInputModel>(managerName);
            var trainers = this.trainerService.AvailableTrainersBySchoolIdNotParticipateInCourse<TrainersViewModel>(school.Id);
            return this.View(trainers);
        }

        // GET: Trainers/Details/5
        public ActionResult Details(int id)
        {
            return this.View();
        }

        // GET: Trainers/Create
        public ActionResult Create(string userId)
        {
            var managerName = this.User.Identity.Name;
            var school = this.schoolService.GetSchoolByManagerName<EditSchoolInputModel>(managerName);
            this.ViewBag.UserId = userId;
            this.ViewBag.SchoolId = school.Id;
            return this.View();
        }

        // POST: Trainers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTrainerInputModel model)
        {
            if (!this.ModelState.IsValid) return this.View();

            this.trainerService.Hire(model);

            return this.RedirectToAction("All", "Trainers");
        }

        //todo Decide how to change trainer user profile. Or add new properties in trainer model to proceed edit.
        //// GET: Trainers/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    
        //    return this.View();
        //}

        //// POST: Trainers/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
            
        //}

        // GET: Trainers/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Trainers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            
        }
    }
}