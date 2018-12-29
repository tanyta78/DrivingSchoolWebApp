namespace DrivingSchoolWebApp.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Lesson;

    public class LessonsController : BaseController
    {

        private readonly ILessonService lessonService;
        private readonly ICustomerService customerService;

        public LessonsController(ILessonService lessonService, ICustomerService customerService)
        {
            this.lessonService = lessonService;
            this.customerService = customerService;
        }

        // GET: Lessons
        public ActionResult Index()
        {
            //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var customer = this.customerService.GetCustomerByUserId(userId);
            //var courseId = 0;
            //if (customer.CoursesOrdered.Count() != 0)
            //{
            //    //?!?!
            //    courseId = customer.CoursesOrdered.FirstOrDefault().Id;

            //}

            //this.ViewBag.CourseId = courseId;
            //this.ViewBag.CustomerId = customer.Id;
            return this.View("CustomerSchedule");
        }

        // GET: Lessons/GetMyEvents
        public ActionResult GetMyEvents()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customerId = this.customerService.GetCustomerByUserId(userId).Id;
            var lessons = this.lessonService.GetLessonsByCustomerId<DetailsLessonViewModel>(customerId).ToList();
            var result = this.Json(new {success = true, lessons});
            return result;
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
            var model = new CreateLessonInputModel()
            {
                OrderId = orderId
            };

            return this.View(model);
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
            return this.RedirectToAction("Details", "Lessons", lesson.Id);


        }
        // POST: Lessons/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FullCalendarInputModel model)
        {

            if (!this.ModelState.IsValid)
            {
                return this.Json(new { success = false, error = true });
            }

            var result = this.lessonService.Save(model);

            return this.RedirectToAction(nameof(Index));

        }

        // POST: Lessons/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAjax(FullCalendarInputModel model)
        {

            if (!this.ModelState.IsValid)
            {
                return this.Json(new { success = false, error = true });
            }

            var result = this.lessonService.Save(model);

            return this.Json(new { success = true, responseText = "Success" });

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
            return this.RedirectToAction("Details", "Lessons", lesson.Id);

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
    }
}