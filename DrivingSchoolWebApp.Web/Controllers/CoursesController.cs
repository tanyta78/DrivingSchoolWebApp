namespace DrivingSchoolWebApp.Web.Controllers
{
    using System;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Car;
    using Services.Models.Course;
    using Services.Models.School;
    using Services.Models.Trainer;

    public class CoursesController : BaseController
    {
        private readonly ICourseService courseService;
        private readonly ITrainerService trainerService;
        private readonly ICarService carService;
        private readonly ISchoolService schoolService;

        public CoursesController(ICourseService courseService, ITrainerService trainerService, ICarService carService, ISchoolService schoolService)
        {
            this.courseService = courseService;
            this.trainerService = trainerService;
            this.carService = carService;
            this.schoolService = schoolService;
        }

        // GET: Courses/All
        public ActionResult All()
        {
            var courses = this.courseService.GetAllCourses<AllCoursesViewModel>();
            return this.View(courses);
        }

        // Post: Courses/All
        [HttpPost]
        public ActionResult All(string category)
        {
            //todo add check for correct category
            var courses = this.courseService.GetCoursesByCategory<AllCoursesViewModel>(Enum.Parse<Category>(category));
            return this.View(courses);
        }

        // GET: Courses/Offered
        public ActionResult Offered()
        {
            var username = this.User.Identity.Name;
            var schoolId = this.schoolService.GetSchoolByManagerName<SchoolViewModel>(username).Id;
            var courses = this.courseService.GetCoursesBySchoolId<OfferedCoursesViewModel>(schoolId);
            return this.View(courses);
        }

        // POST: Courses/Offered
        [HttpPost]
        public ActionResult Offered(string category)
        {
            var username = this.User.Identity.Name;
            var schoolId = this.schoolService.GetSchoolByManagerName<SchoolViewModel>(username).Id;
            var courses = this.courseService.GetCoursesBySchoolIdAndCategory<OfferedCoursesViewModel>(schoolId,Enum.Parse<Category>(category));

            return this.View(courses);
        }

        // GET: Courses/Details/5
        public ActionResult Details(int id)
        {
            var course = this.courseService.GetCourseById<DetailsCourseViewModel>(id);

            return this.View(course);
        }

        // GET: Courses/Create
        public ActionResult Create(int? trainerId)
        {
            var username = this.User.Identity.Name;
            var schoolId = this.schoolService.GetSchoolByManagerName<SchoolViewModel>(username).Id;
            this.ViewBag.SchoolId = schoolId;
            this.ViewBag.TrainerId = trainerId;
            this.ViewBag.Trainers =this.trainerService
                .AvailableTrainersBySchoolIdNotParticipateInCourse<AvailableTrainerViewModel>(
                    schoolId);
            this.ViewBag.Cars = this.carService.GetCarsBySchoolId<CarViewModel>(schoolId);
            return this.View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCourseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var course = this.courseService.Create(model);
            return this.RedirectToAction("Details", "Courses", course.Id);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return this.RedirectToAction("All");
            }
            catch
            {
                return this.View();
            }
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Courses/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return this.RedirectToAction("All");
            }
            catch
            {
                return this.View();
            }
        }
    }
}