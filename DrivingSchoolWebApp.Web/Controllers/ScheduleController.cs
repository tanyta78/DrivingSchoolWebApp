namespace DrivingSchoolWebApp.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Lesson;

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : BaseController
    {
        private readonly ILessonService lessonService;
        private readonly ICustomerService customerService;

        public ScheduleController(ILessonService lessonService, ICustomerService customerService)
        {
            this.lessonService = lessonService;
            this.customerService = customerService;
        }

       
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Lesson>), 200)]
        public ActionResult<List<DetailsLessonViewModel>> GetMyEvents()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customerId = this.customerService.GetCustomerByUserId(userId).Id;
            return this.lessonService.GetLessonsByCustomerId<DetailsLessonViewModel>(customerId).ToList();

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Lesson), 200)]
        [ProducesResponseType(404)]
        public ActionResult<DetailsLessonViewModel> GetByIdAsync(int id)
        {
            var lesson = this.lessonService.GetLessonById<DetailsLessonViewModel>(id);

            if (lesson == null)
            {
                return this.NotFound();
            }

            return lesson;
        }

       
        [HttpPost]
        [ProducesResponseType(typeof(Lesson), 201)]
        [ProducesResponseType(400)]
        public ActionResult SaveAjax([FromBody]FullCalendarInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = this.lessonService.Save(model);

            return this.CreatedAtAction(nameof(GetByIdAsync),
                new { id = model.Id }, model);

        }

        //// GET: Lessons/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return this.View();
        //}

        //// POST: Lessons/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(EditLessonInputModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }

        //    var lesson = this.lessonService.Edit(model);
        //    return this.RedirectToAction("Details", "Lessons", lesson.Id);

        //}

        //// GET: Lessons/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return this.View();
        //}

        //// POST: Lessons/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    this.lessonService.Delete(id);
        //    return this.RedirectToAction(nameof(Index));
        //}
    }
}