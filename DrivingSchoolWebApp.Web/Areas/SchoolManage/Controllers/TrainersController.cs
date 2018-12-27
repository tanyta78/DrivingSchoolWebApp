namespace DrivingSchoolWebApp.Web.Areas.SchoolManage.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.School;
    using Services.Models.Trainer;
    using Web.Controllers;
    using X.PagedList;

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
        public ActionResult All(int? page)
        {
            var managerName = this.User.Identity.Name;
            var school = this.schoolService.GetSchoolByManagerName<EditSchoolInputModel>(managerName);
            var trainers = this.trainerService.TrainersBySchoolId<AllTrainerViewModel>(school.Id);

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            IPagedList<AllTrainerViewModel> model = trainers.ToPagedList(pageNumber, 5); // will only contain 5 products max because of the pageSize

            return this.View(model);
        }

        // GET: Trainers/Available
        public ActionResult Available(int? page)
        {
            var managerName = this.User.Identity.Name;
            var school = this.schoolService.GetSchoolByManagerName<EditSchoolInputModel>(managerName);
            var trainers = this.trainerService.AvailableTrainersBySchoolIdNotParticipateInCourse<AvailableTrainerViewModel>(school.Id);

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            IPagedList<AvailableTrainerViewModel> model = trainers.ToPagedList(pageNumber, 5); // will only contain 5 products max because of the pageSize

            return this.View(model);
        }

        // GET: Trainers/Details/5
        public ActionResult Details(int id)
        {
            var trainer = this.trainerService.GetTrainerById<DetailsTrainerViewModel>(id);

            return this.View(trainer);
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
            var trainer = this.trainerService.GetTrainerById<DeleteTrainerViewModel>(id);

            //return this.View(trainer);
            return this.RedirectToAction("All");

        }

        // POST: Trainers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            this.trainerService.Delete(id);
            return this.RedirectToAction("All");
        }
    }
}