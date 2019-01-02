namespace DrivingSchoolWebApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
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
            try
            {
                var courses = new List<AllCoursesViewModel>();
                if (category == "All")
                {
                    courses = this.courseService.GetAllCourses<AllCoursesViewModel>().ToList();
                }
                else
                {
                    courses = this.courseService.GetCoursesByCategory<AllCoursesViewModel>(Enum.Parse<Category>(category)).ToList();
                }

                return this.View(courses);
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
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
            var courses = new List<OfferedCoursesViewModel>();
            if (category == "All")
            {
                courses = this.courseService.GetCoursesBySchoolId<OfferedCoursesViewModel>(schoolId).ToList();
            }
            else
            {
                courses = this.courseService.GetCoursesBySchoolIdAndCategory<OfferedCoursesViewModel>(schoolId, Enum.Parse<Category>(category)).ToList();
            }

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
            this.ViewBag.Trainers = this.trainerService
                .TrainersBySchoolId<AvailableTrainerViewModel>(
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

            try
            {
                var course = this.courseService.Create(model).GetAwaiter().GetResult();
                return this.RedirectToAction("Details", "Courses", new { Area = "", id = course.Id });
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var username = this.User.Identity.Name;
                var schoolId = this.schoolService.GetSchoolByManagerName<SchoolViewModel>(username).Id;
                this.ViewBag.Trainers = this.trainerService
                    .TrainersBySchoolId<AvailableTrainerViewModel>(
                        schoolId);
                this.ViewBag.Cars = this.carService.GetCarsBySchoolId<CarViewModel>(schoolId);

                var model = this.courseService.GetCourseById<EditCourseInputModel>(id);
                return this.View(model);
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCourseInputModel model)
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

                model.Username = this.User.Identity.Name;
                var course = this.courseService.Edit(model).GetAwaiter().GetResult();
                return this.RedirectToAction("Details", "Courses", new { Area = "", id = course.Id });
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var course = this.courseService.GetCourseById<DeleteCourseViewModel>(id);

                return this.View(course);

            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // POST: Courses/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (!this.HasRights(id))
                {
                    throw new OperationCanceledException("You do not have rights for this operation!");
                }

                this.courseService.Delete(id).GetAwaiter().GetResult();

                return this.RedirectToAction(nameof(this.All));
            }
            catch
            {
                return this.View();
            }
        }

        private bool HasRights(int courseId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var course = this.courseService.GetCourseById<DetailsCourseViewModel>(courseId);

            var result = (userId == course.SchoolManagerId) || this.User.IsInRole("Admin");
            return result;
        }
    }
}