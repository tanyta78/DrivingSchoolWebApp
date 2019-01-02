namespace DrivingSchoolWebApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Lesson;
    using Services.Models.Order;
    using Services.Models.School;
    using Services.Models.Trainer;

    public class LessonsController : BaseController
    {

        private readonly ILessonService lessonService;
        private readonly ICustomerService customerService;
        private readonly IOrderService orderService;
        private readonly ISchoolService schoolService;
        private readonly ITrainerService trainerService;

        public LessonsController(ILessonService lessonService, ICustomerService customerService, IOrderService orderService, ISchoolService schoolService, ITrainerService trainerService)
        {
            this.lessonService = lessonService;
            this.customerService = customerService;
            this.orderService = orderService;
            this.schoolService = schoolService;
            this.trainerService = trainerService;
        }

        // GET: Lessons
        public ActionResult Index(int trainerId, int orderId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (this.User.IsInRole("School"))
            {
                var school = this.schoolService.GetSchoolByManagerName<SchoolViewModel>(this.User.Identity.Name);
                var trainers = this.trainerService.TrainersBySchoolId<AvailableTrainerViewModel>(school.Id);

                var receivedOrders = this.orderService.GetOrdersBySchoolIdPaymentMadeAndTrainerId<FullCalendarOrdersViewModel>(school.Id, trainerId)
                    .ToList();

                this.ViewBag.OrdersList = receivedOrders;
                this.ViewBag.TrainersList = trainers;
                this.ViewBag.TrainerId = trainerId;
                this.ViewBag.OrderId = orderId;
                return this.View("SchoolSchedule");
            }
            else
            {
                var customer = this.customerService.GetCustomerByUserId(userId);

                var orders = this.orderService.GetOrdersByCustomerId<FullCalendarOrdersViewModel>(customer.Id).ToList();

                this.ViewBag.OrdersList = orders;

                return this.View("CustomerSchedule");
            }

        }

        // GET: Lessons/GetMyEvents
        public ActionResult<List<DetailsLessonViewModel>> GetMyEvents()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customerId = this.customerService.GetCustomerByUserId(userId).Id;
            var lessons = this.lessonService.GetLessonsByCustomerId<DetailsLessonViewModel>(customerId).ToList();

            return lessons;
        }

        // GET: Lessons/GetSchoolEvents
        public ActionResult<List<DetailsLessonViewModel>> GetSchoolEvents(int? trainerId)
        {
            var schoolId = this.schoolService.GetSchoolByManagerName<SchoolViewModel>(this.User.Identity.Name).Id;

            var trainers = this.trainerService.TrainersBySchoolId<AvailableTrainerViewModel>(schoolId);

            var lessons = this.lessonService.GetLessonsByTrainerId<DetailsLessonViewModel>(trainerId ?? trainers.First().Id).ToList();

            return lessons;
        }

        // GET: Lessons/Details/5
        public ActionResult Details(int id)
        {
            var lesson = this.lessonService.GetLessonById<DetailsLessonViewModel>(id);
            return this.View(lesson);
        }

        // GET: Lessons/Create/4
        public ActionResult Create(int orderId)
        {
            var order = this.orderService.GetOrderById<DetailsOrderViewModel>(orderId);
            var trainerId = order.CourseTrainerId;
            return this.RedirectToAction("Index", new { trainerId = trainerId, orderId = orderId });

        }

        // POST: Lessons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLessonInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var lesson = this.lessonService.Create(model).GetAwaiter().GetResult();
            return this.RedirectToAction("Details", "Lessons", new { lessonId = lesson.Id });


        }
        // POST: Lessons/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FullCalendarInputModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.Json(new { success = false, error = true });
                }

                if (!this.HasRights(model.Id))
                {
                    throw new OperationCanceledException("You do not have rights for this operation!");
                }

                var result = this.lessonService.Save(model);

                return this.RedirectToAction("Index", new { trainerId = model.TrainerId });
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }

        }


        // GET: Lessons/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Lessons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLessonInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var lesson = this.lessonService.Edit(model);
            return this.RedirectToAction("Details", "Lessons", new { lessonId = lesson.Id });

        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Lessons/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            this.lessonService.Delete(id);
            return this.RedirectToAction(nameof(Index));
        }

        private bool HasRights(int lessonId)
        {
            if (lessonId==0)
            {
                return true;
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lesson = this.lessonService.GetLessonById<DetailsLessonViewModel>(lessonId);

            var result = (userId == lesson.OrderCourseSchoolManagerUserId) || this.User.IsInRole("Admin");
            return result;
        }
    }
}