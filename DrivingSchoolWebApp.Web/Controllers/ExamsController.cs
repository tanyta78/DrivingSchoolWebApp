namespace DrivingSchoolWebApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Mvc;
    using Services.DataServices.Contracts;
    using Services.Models.Certificate;
    using Services.Models.Customer;
    using Services.Models.Exam;
    using Services.Models.Order;
    using Services.Models.School;

    public class ExamsController : BaseController
    {
        private readonly IExamService examService;
        private readonly IOrderService orderService;
        private readonly ICertificateService certificateService;
        private readonly ICustomerService customerService;
        private readonly ISchoolService schoolService;

        public ExamsController(IExamService examService, IOrderService orderService, ICertificateService certificateService, ICustomerService customerService, ISchoolService schoolService)
        {
            this.examService = examService;
            this.orderService = orderService;
            this.certificateService = certificateService;
            this.customerService = customerService;
            this.schoolService = schoolService;
        }

        // GET: Exams/All
        public ActionResult All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exams = new List<AllExamsViewModel>();
            if (this.User.IsInRole("Admin"))
            {
                exams = this.examService.All<AllExamsViewModel>().ToList();
            }
            else if (this.User.IsInRole("School"))
            {
                var schoolId = this.schoolService
                    .GetSchoolByManagerName<DetailsSchoolViewModel>(this.User.Identity.Name).Id;
                exams = this.examService.GetExamsBySchoolId<AllExamsViewModel>(schoolId).ToList();
            }
            else
            {
                var customerId = this.customerService.GetCustomerByUserId<DetailsCustomerViewModel>(userId).Id;
                exams = this.examService.GetExamsByCustomerId<AllExamsViewModel>(customerId).ToList();
            }
            return this.View(exams);
        }

        // GET: Exams/Create/3
        public ActionResult Create(int orderId)
        {
            try
            {
                var order = this.orderService.GetOrderById<DetailsOrderViewModel>(orderId);
                var model = new CreateExamInputModel()
                {
                    CourseId = order.CourseId,
                    CustomerId = order.CustomerId
                };
                return this.View(model);
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // POST: Exams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateExamInputModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                var exam = this.examService.Create(model).GetAwaiter().GetResult();

                return this.RedirectToAction(nameof(All));
            }
            catch
            {
                return this.View(model);
            }
        }

        // POST: Exams/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int id)
        {
            try
            {
                if (!this.HasRights(id))
                {
                    throw new OperationCanceledException("You do not have rights for this operation!");
                }

                var exam = this.examService.CancelExam(id).GetAwaiter().GetResult();

                return this.RedirectToAction(nameof(All));
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        // POST: Exams/Finish/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finish(int id)
        {
            try
            {
                if (!this.HasRights(id))
                {
                    throw new OperationCanceledException("You do not have rights for this operation!");
                }

                var exam = this.examService.ChangeStatus(id, LessonStatus.Finished).GetAwaiter().GetResult();

                var model = new CreateCertificateInputModel()
                {
                    CourseId = exam.CourseId,
                    CustomerId = exam.CustomerId
                };

                var certificate = this.certificateService.Create(model).GetAwaiter().GetResult();

                return this.RedirectToAction("Details", "Certificates", new { id = certificate.Id });
            }
            catch (Exception e)
            {
                return this.View("_Error", e.Message);
            }
        }

        private bool HasRights(int examId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exam = this.examService.GetExamById<AllExamsViewModel>(examId);

            var result = (userId == exam.CourseSchoolManagerId) || this.User.IsInRole("Admin");
            return result;
        }
    }
}